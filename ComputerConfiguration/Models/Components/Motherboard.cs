using ComputerConfiguration.Models.Build;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Models.Components
{
    public class Motherboard : ComponentBase
    {
        public string Chipset { get; set; }
        public string MemoryType { get; set; }
        public int MemorySlots { get; set; }
        public int MaxMemory { get; set; }
        public int MaxMemorySpeed { get; set; }
        public string PcieVersion { get; set; }
        public int M2Slots { get; set; }
        public int SataPorts { get; set; }
        public bool IntegratedWifi { get; set; }
        public bool IntegratedBluetooth { get; set; }
        public List<ComputerBuild> ComputerBuilds { get; set; }
        public string Socket { get; set; }
        public string FormFactor { get; set; }
        public int PowerConsumption { get; set; }
    }
}
