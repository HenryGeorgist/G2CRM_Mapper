using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperView
{
    public class MappedOutputVM
    {
        public bool UseMappedOutput { get; set; }
        public bool MappedOutputAlreadyPresentInMap { get; set; }
        public string Representation { get; set; }
        public string Alternative { get; set; }
        public string SimulationName { get; set;}
        public string SeaLevelChange { get; set; }
        public MappedOutputVM(string representation, string alternative, string simName, string seaLevelChange)
        {
            Representation = representation;
            Alternative = alternative;
            SimulationName = simName;
            SeaLevelChange = seaLevelChange;
        }
        public string MappedOuputPath(string rootDir)
        {
            return rootDir + "/" + Representation.Replace(' ','_') + "/" + Alternative + "/" + SimulationName + "/" + "MapOutputs_" + SeaLevelChange + "_" + SimulationName + ".sqlite";
        }
    }
}
