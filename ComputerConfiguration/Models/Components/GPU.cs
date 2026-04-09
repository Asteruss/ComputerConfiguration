using ComputerConfiguration.Models.Build;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Models.Components
{
    public class Gpu : ComponentBase
    {
        //public int Id { get; set; }

        public int MemorySize { get; set; }
        public string MemoryType { get; set; }
        public int CoreClock { get; set; }
        public int BoostClock { get; set; }
        public int Tdp { get; set; }
        public int Length { get; set; }
        public string PowerConnectors { get; set; }
        public int HdmiPorts { get; set; }
        public int DisplayPorts { get; set; }
        public List<ComputerBuild> ComputerBuilds { get; set; }
    }
}
