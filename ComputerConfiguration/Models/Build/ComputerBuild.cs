using ComputerConfiguration.Commands;
using ComputerConfiguration.Models.Components;
using ComputerConfiguration.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Models.Build
{
    public class ComputerBuild
    {
        public int Id { get; set; }
        public int? CaseId { get; set; }
        public int? CoolerId { get; set; }
        public int? CPUId { get; set; }
        public int? GPUId { get; set; }
        public int? MotherboardId { get; set; }
        public int? PsuId { get; set; }
        public Cpu? Cpu { get; set; }
        public Gpu? Gpu { get; set; }
        public Motherboard? Motherboard { get; set; }
        public Case? Case { get; set; }
        public Cooler? Cooler { get; set; }
        public Psu? Psu { get; set; }
        public List<BuildRam> BuildRams { get; set; } = new();
        public List<BuildStorage> BuildStorages { get; set; } = new();
        public List<AdditionalServiceOption>? AdditionalServices { get; set; } = new();
        public List<Order>? Orders { get; set; } = new();
        public int? OrderId { get; set; }
    }
}
