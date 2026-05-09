using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ComputerConfiguration.Models.Components
{
    public class Psu : ComponentBase
    {
        public int Wattage { get; set; }
        public EfficiencyRating EfficiencyRating { get; set; }
        public bool Modular { get; set; }
        public int SataConnectors { get; set; }
        public int PcieConnectors { get; set; }
        public List<ComputerBuild> ComputerBuilds { get; set; }
    }
}
