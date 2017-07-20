using System;
using System.Collections.Generic;
using System.Windows;

namespace MapperView
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private string _MapDirectory = System.IO.Directory.GetCurrentDirectory();

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Mapper m = new Mapper();
            m.Closing += M_Closing;
            m.Show();
            if (e.Args.Length == 3)
            {
                string outputDB = e.Args[0];
                string assetDB = e.Args[1];

                if (outputDB == null)
                {
                    LoadFromXml(m);
                    return;
                }
                if (outputDB == "")
                {
                    LoadFromXml(m);
                    return;
                }
                //parse the result and add the maps to the map window.
                string root = System.IO.Directory.GetParent(outputDB).FullName;
                _MapDirectory = root;
                //check for mapper.xml
                LoadFromXml(m);
                //determine if any mapped outputs exist in the currently loaded map and remove them from the mapped output list?
                List<OpenGLMapping.FeatureNode> nodes = m.MapTreeView.GetAllFeatureNodes();

                MappedOutputListVM mappedOutputs = new MappedOutputListVM(outputDB);
                foreach (MappedOutputVM mo in mappedOutputs.Items)
                {
                    //check if the output file is already created.
                    string sqlitefile = mo.MappedOuputPath(root);
                    string outputdir = System.IO.Directory.GetParent(sqlitefile).FullName;
                    outputdir = outputdir + "/MapperFiles/";
                    //if (!System.IO.Directory.Exists(outputdir)) System.IO.Directory.CreateDirectory(outputdir);
                    string shpfile = outputdir + mo.SimulationName + "_AssetDamages.shp";
                    if (System.IO.File.Exists(shpfile))
                    {
                        foreach (OpenGLMapping.FeatureNode fn in nodes)
                        {
                            if (fn.Features.Features != null)
                            {
                                if (fn.Features.Features.GetSource == shpfile)
                                {
                                    //dont let that mapped output continue.
                                    mo.MappedOutputAlreadyPresentInMap = true;
                                }
                            }

                        }
                    }
                }
                BrowserView bv = new BrowserView();
                bv.DataContext = mappedOutputs;
                bv.Owner = m;
                bv.ShowDialog();

                foreach (MappedOutputVM mo in mappedOutputs.Items)
                {
                    if (mo.UseMappedOutput)
                    {


                        string sqlitefile = mo.MappedOuputPath(root);

                        if (!System.IO.File.Exists(sqlitefile))
                        {
                            System.Windows.MessageBox.Show(sqlitefile + " doesn't exist.");
                            return;
                        }
                        if (!System.IO.Path.GetExtension(sqlitefile).Equals(".sqlite"))
                        {
                            System.Windows.MessageBox.Show(sqlitefile + " is not a .sqlite file type.");
                            return;
                        }
                        if (!sqlitefile.Contains("MapOutputs_"))
                        {
                            System.Windows.MessageBox.Show(sqlitefile + " isn't a G2CRM mapped outputs file.");
                            return;
                        }
                        try
                        {
                            DataBase_Reader.SQLiteReader reader = new DataBase_Reader.SQLiteReader(sqlitefile);
                            reader.SetTableReader("Assets");
                            reader.Open();
                            //string[] colnames = reader.ColumnNames;
                            LifeSimGIS.PointFeatures p = new LifeSimGIS.PointFeatures();
                            int count = (int)reader.GetRowCount("Assets");
                            string[] desc = new string[count];
                            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            for (int i = 0; i < count; i++)
                            {
                                object[] rowdata = reader.GetRow(i);
                                desc[i] = (string)rowdata[2];
                                byte[] bytes = (byte[])rowdata[7];
                                double x = BitConverter.ToDouble(bytes, 43);
                                double y = BitConverter.ToDouble(bytes, 43 + 8);
                                p.Points.Add(new LifeSimGIS.PointD(x, y));
                                //sb.AppendLine("Point(" + x + " " + y);
                            }
                            p.CalculateExtent();
                            p.NRecords = p.Points.Count;
                            //string result = sb.ToString();
                            reader.SetTableReader("Statistics");
                            Int64[] TimesWet = new Int64[count];
                            double[] MeanDamage = new double[count];
                            double[] StdevDamage = new double[count];
                            double[] SkewDamage = new double[count];
                            //double[] KurtDamage = new double[count];
                            //string[] structurecols = reader.ColumnNames;
                            count = (int)reader.GetRowCount("Statistics");
                            string[] statstypes = new string[count];
                            for (int i = 0; i < count; i++)
                            {
                                object[] statsrow = reader.GetRow(i);
                                statstypes[i] = (string)statsrow[5];
                                if (statstypes[i] == "PVDamage")
                                {
                                    string ID1 = (string)statsrow[0];
                                    if (ID1.Equals("Asset"))
                                    {
                                        int ID4;
                                        Int32.TryParse((string)statsrow[3], out ID4);
                                        //Int32.TryParse((string)statsrow[3],out ID4);
                                        TimesWet[ID4 - 1] = (Int64)statsrow[8];
                                        MeanDamage[ID4 - 1] = (double)(Decimal)statsrow[9];
                                        StdevDamage[ID4 - 1] = (double)(Decimal)statsrow[10];
                                        SkewDamage[ID4 - 1] = (double)(Decimal)statsrow[11];
                                        //KurtDamage[ID4-1] = (double)(Decimal)statsrow[12];
                                    }
                                }
                            }
                            string outputdir = System.IO.Directory.GetParent(sqlitefile).FullName;
                            outputdir = outputdir + "/MapperFiles/";
                            if (!System.IO.Directory.Exists(outputdir)) System.IO.Directory.CreateDirectory(outputdir);
                            string shpfile = outputdir + mo.SimulationName + "_AssetDamages.shp";
                            if (mo.MappedOutputAlreadyPresentInMap)
                            {
                                if (System.Windows.Forms.MessageBox.Show("The file " + shpfile + " already exists in the map window, proceeding with an import could cause undesired results, are you sure you wish to proceed?", "Danger", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                                {
                                    break;
                                }
                            }
                            if (System.IO.File.Exists(shpfile))
                            {
                                if (System.Windows.MessageBox.Show(shpfile + " already exists, would you like to delete?", "Existing Output", System.Windows.MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                                {
                                    System.IO.File.Delete(shpfile);
                                    if (System.IO.File.Exists(System.IO.Path.ChangeExtension(shpfile, ".dbf"))) System.IO.File.Delete(System.IO.Path.ChangeExtension(shpfile, ".dbf"));
                                    if (System.IO.File.Exists(System.IO.Path.ChangeExtension(shpfile, ".shx"))) System.IO.File.Delete(System.IO.Path.ChangeExtension(shpfile, ".shx"));
                                    if (System.IO.File.Exists(System.IO.Path.ChangeExtension(shpfile, ".prj"))) System.IO.File.Delete(System.IO.Path.ChangeExtension(shpfile, ".prj"));

                                    //remove the previous instance from the map window before addnig it again.
                                    if (mo.MappedOutputAlreadyPresentInMap)
                                    {
                                        foreach (OpenGLMapping.FeatureNode fntoremove in nodes)
                                        {
                                            if (fntoremove.Features.Features != null)
                                            {
                                                if (fntoremove.Features.Features.GetSource == shpfile)
                                                {
                                                    //dont let that mapped output continue.
                                                    fntoremove.RemoveLayer(true);
                                                }
                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    return;
                                }

                            }

                            LifeSimGIS.ShapefileWriter sw = new LifeSimGIS.ShapefileWriter(shpfile);
                            System.Collections.Generic.List<Array> data = new List<Array>();
                            data.Add(desc);
                            data.Add(TimesWet);
                            data.Add(MeanDamage);
                            data.Add(StdevDamage);
                            data.Add(SkewDamage);
                            //data.Add(KurtDamage);
                            System.Collections.Generic.List<string> colheaders = new List<string>();
                            colheaders.Add("Name");
                            colheaders.Add("N");
                            colheaders.Add("MeanPVDmg");
                            colheaders.Add("StDevPVDmg");
                            colheaders.Add("SkewPVDmg");
                            //colheaders.Add("KurtPVDmg");
                            int proj = 0;
                            if (Int32.TryParse(e.Args[2], out proj))
                            {
                                sw.WriteFeatures(p, data, colheaders, new GDALAssist.EPSGProjection(proj));
                            }
                            else
                            {
                                sw.WriteFeatures(p, data, colheaders);
                            }



                            OpenGLMapping.FeatureNode fn = new OpenGLMapping.FeatureNode(shpfile, m.MapWindow.MapWindow);
                            m.MapTreeView.AddGISData(fn);
                        }catch(Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show("Errors opening Mapped Output sqlite file, " + sqlitefile + "\n" + ex.Message.ToString());
                        }


                    }

                }
                try
                {
                    //check if open street map already exists in the current map..
                    foreach (OpenGLMapping.FeatureNode fn in nodes)
                    {
                        if (fn.Features.Features == null)
                        {
                            if (fn.FeatureNodeHeader == "OpenStreetMap")
                            {
                                return;
                            }
                        }

                    }
                    OpenGLMapping.FeatureNode fn2 = new OpenGLMapping.FeatureNode(new OpenGLMapping.MapWebTiles(m.MapWindow.MapWindow, OpenGLMapping.MapWebTiles.WebMapSource.OpenStreetMap), "OpenStreetMap", m.MapWindow.MapWindow);
                    m.MapTreeView.AddGISData(fn2, m.MapTreeView.Items.Count, true);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
            else if (e.Args.Length == 1)
            {
                //check for mapper.xml file
                _MapDirectory = e.Args[0];
                if (!System.IO.Directory.Exists(_MapDirectory)) System.IO.Directory.CreateDirectory(_MapDirectory);
                LoadFromXml(m);
            }
            else
            {
                LoadFromXml(m);
            }
        }

        private void M_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("Would you like to save your map settings?", "Save Map?", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                Mapper m = (Mapper)sender;
                System.Xml.Linq.XElement ele = m.MapTreeView.WriteToXElement(_MapDirectory);
                System.Xml.Linq.XDocument doc = new System.Xml.Linq.XDocument(ele);
                doc.Save(_MapDirectory + "/mapper.xml");
            }

        }
        private void LoadFromXml(Mapper m)
        {
            if (System.IO.File.Exists(_MapDirectory + "/mapper.xml"))
            {
                //determine if the user would like to load the file?
                if (System.Windows.Forms.MessageBox.Show("A previously loaded map file exists, would you like to load it again?", "Reload Map?", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    byte[] ba = System.IO.File.ReadAllBytes(_MapDirectory + "/mapper.xml");
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(ba);
                    System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Load(ms);
                    ms.Dispose();
                    System.Xml.Linq.XElement ele = doc.Root;
                    m.MapTreeView.LoadFromXElement(ele, _MapDirectory);
                }


            }

        }
    }
}
