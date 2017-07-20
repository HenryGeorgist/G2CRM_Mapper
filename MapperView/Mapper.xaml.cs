using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MapperView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Mapper : Window
    {
        public Mapper()
        {
            InitializeComponent();
            try
            {
                string path = "";
                string dir = "";
                bool itworked = true;
                try
                {
                    path = Environment.GetEnvironmentVariable("GDAL_DATA");
                }catch(Exception except)
                {
                    System.Windows.MessageBox.Show(except.Message.ToString());
                    itworked = false;
                }
                if (itworked & path!=null)
                {
                    dir = path;
                }else
                {
                    dir = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                    dir = new Uri(dir).LocalPath;
                    dir = System.IO.Path.GetDirectoryName(dir);
                }
                Environment.SetEnvironmentVariable("GDAL_TIFF_OVR_BLOCKSIZE", "256");
                string ToolDir = dir + "\\GDAL\\bin";
                string DataDir = dir + "\\GDAL\\data";
                string PluginDir = dir + "\\GDAL\\bin\\gdalplugins";
                string WMSDir = dir + "\\GDAL\\Web Map Services";
                GDALAssist.GDALSetup.Initialize(ToolDir, DataDir, PluginDir, WMSDir);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message.ToString());
                //Messager.Logger.Instance.ReportMessage(new Messager.ErrorMessage(ex.InnerException.ToString() + "\n Failed to initialize GDAL, check if the GDAL directory is next to the FdaModel.dll", Messager.ErrorMessageEnum.Fatal | Messager.ErrorMessageEnum.Model));
            }

            MapWindow.MapWindow.TreeView = MapTreeView;
            MapTreeView.MapWindow = MapWindow.MapWindow;
            MapToolbar.MapWindow = MapWindow.MapWindow;
            MapToolbar.MapTree = MapTreeView;
            SelectableLayers.MapTree = MapTreeView;
            SelectableLayers.MapWindow = MapWindow.MapWindow;
            FeatureEditorToolbar.MapWindow = MapWindow.MapWindow;
            FeatureEditorToolbar.MapTree = MapTreeView;
            StatusBorder.Child = new OpenGLMapping.MapStatusBar(MapWindow.MapWindow);

            RadioButton ArrowTool = (RadioButton)MapToolbar.Items[0];
            ArrowTool.IsChecked = true;
            MapToolbar.RadioChecked += MapToolBar_RadioChecked;

            //event handlers

            //MapTreeView.LayerAdded += MapTreeView_LayerAdded; //adds menu item to the remove Layer menu item items.
            //MapTreeView.LayerRemoved += MapTreeView_LayerRemoved; //Removes menu item for layer from the Remove Layer menu item items.
            //MapTreeView.LayerMoved += MapTreeView_LayerMoved;//reorders main menu item for RemoveLayer

            FeatureEditorToolbar.RadioChecked += FeatureEditorToolbar_RadioChecked;

            //MapWindow.MapWindow.MouseLeave += MapWindow_Mouse;
            //MapWindow.MapWindow.MouseEnter += MapWindow_Mouse;
        }

        private void MapToolBar_RadioChecked(RadioButton buttonChecked)
        {
            foreach (object item in FeatureEditorToolbar.Items)
            {
                RadioButton r = item as RadioButton;
                if (r != null)
                {
                    r.IsChecked = false;
                }
            }
        }

        private void MapWindow_Mouse(object sender, EventArgs e)
        {
            object fe = FocusManager.GetFocusedElement(this);
            if (fe == null) return;
            if (fe.GetType() == typeof(OpenGLMapping.WinFormsHostControl)) { MapWindow.MapWindow.Focus(); }
        }
        private void FeatureEditorToolbar_RadioChecked(RadioButton buttonChecked)
        {
            if (buttonChecked == null)
            {
                MapToolbar.SelectChecked();
            }
            else
            {
                foreach (object item in MapToolbar.Items)
                {
                    RadioButton r = item as RadioButton;
                    if (r != null)
                    {
                        r.IsChecked = false;
                    }
                }
            }
        }
    }
}
