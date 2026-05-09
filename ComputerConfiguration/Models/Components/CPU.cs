using ComputerConfiguration.Models.Build;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Models.Components
{
    public class Cpu : ComponentBase
    {
        public int CoreCount { get; set; }
        public int ThreadCount { get; set; }
        public double BaseClock { get; set; }
        public double BoostClock { get; set; }
        public bool IntegratedGraphics { get; set; }
        public string Series { get; set; }
        public int MaxMemorySpeed { get; set; }
        public string Socket { get; set; }
        public int Tdp { get; set; }
        public List<ComputerBuild> ComputerBuilds { get; set; }
    }
}
