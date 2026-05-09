using ComputerConfiguration.Models.Build;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Models.Components
{
    public class Ram : ComponentBase
    {
        public string MemoryType { get; set; }
        public int Capacity { get; set; }
        public int Speed { get; set; }
        public int ModuleCount { get; set; }
        public int TotalCapacity => Capacity * ModuleCount;
        public string Timing { get; set; }
        public double Voltage { get; set; }
        public List<ComputerBuild> ComputerBuilds { get; set; }
    }
}
