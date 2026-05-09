using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Models.Components
{
    public class Case : ComponentBase
    {
        public string Color { get; set; }
        public int MaxGpuLength { get; set; }
        public int MaxCoolerHeight { get; set; }
        public int IncludedFans { get; set; }
        public string RadiatorSupport { get; set; }
        public string SidePanel { get; set; }
        public CaseFormFactor CaseSize { get; set; }
        public string SupportedMotherboardFormFactors { get; set; }
        public int MaxGpuWidthSlots { get; set; }
        public List<ComputerBuild> ComputerBuilds { get; set; }
    }
}
