using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ComputerConfiguration.Models.Components
{
    public class Cooler : ComponentBase
    {
        public CoolerType CoolerType { get; set; }
        public List<CoolerSocket> SocketSupport { get; set; } = new();
        public int TdpRating { get; set; }
        public int Height { get; set; }
        public int RadiatorSize { get; set; }
        public double NoiseLevel { get; set; }
        public List<ComputerBuild> ComputerBuilds { get; set; }
    }
}
