using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Models.Components
{
    public class Storage : ComponentBase
    {
        public StorageType StorageType { get; set; }
        public int Capacity { get; set; }
        public string Interface { get; set; }
        public int ReadSpeed { get; set; }
        public int WriteSpeed { get; set; }
        public List<ComputerBuild> ComputerBuilds { get; set; }
        public string FormFactor { get; set; }
    }
}
