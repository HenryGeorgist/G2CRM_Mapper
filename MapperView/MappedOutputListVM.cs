using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperView
{
    public class MappedOutputListVM
    {
        public System.Collections.ObjectModel.ObservableCollection<MappedOutputVM> Items { get; set; }
        public MappedOutputListVM(string outputDatabase)
        {
            DataBase_Reader.SQLiteReader reader = new DataBase_Reader.SQLiteReader(outputDatabase);
            reader.SetTableReader("SimulationRunLog");
            reader.Open();
            int[] indexes = new int[6];
            string[] colNames = reader.ColumnNames;
            indexes[0] = Array.IndexOf(colNames, "Representation");
            indexes[1] = Array.IndexOf(colNames, "Name");
            indexes[2] = Array.IndexOf(colNames, "HPSAlternative");
            indexes[3] = Array.IndexOf(colNames, "LowSLCSelected");
            indexes[4] = Array.IndexOf(colNames, "IntermediateSLCSelected");
            indexes[5] = Array.IndexOf(colNames, "HighSLCSelected");
            string seaLevelChangeScenario = "";
            object[] row;
            Items = new System.Collections.ObjectModel.ObservableCollection<MappedOutputVM>();
            for(int i =0; i < reader.NumberOfRows; i++)
            {
                row = reader.GetRow(i, indexes);
                if((bool)row[3])
                {
                    seaLevelChangeScenario = "Low";
                }
                else if((bool)row[4])
                {
                    seaLevelChangeScenario = "Intermediate";
                }
                else
                {
                    seaLevelChangeScenario = "High";
                }

                Items.Add(new MappedOutputVM((string)row[0], (string)row[2], (string)row[1], seaLevelChangeScenario));
            }
        }
    }
}
