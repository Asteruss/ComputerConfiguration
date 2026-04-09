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
    public class ComputerBuild : IBuild
    {
        public int Id { get; set; }
        public int CaseId { get; set; }
        public int CoolerId { get; set; }
        public int CPUId { get; set; }
        public int GPUId { get; set; }
        public int MotherboardId { get; set; }
        public int PsuId { get; set; }
        public Cpu? Cpu { get; set; }
        public Gpu? Gpu { get; set; }
        public Motherboard? Motherboard { get; set; }
        public List<Ram>? Rams { get; set; }
        public Case? Case { get; set; }
        public Cooler? Coolor { get; set; }
        public Psu? Psu { get; set; }
        public List<Storage>? Storages { get; set; }
        public List<AdditionalService>? AdditionalServices { get; set; }
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
        public double GetTotalPrice()
        {
            double total = 0;
            if (Cpu != null) total += Cpu.BasePrice;
            if (Gpu != null) total += Gpu.BasePrice;
            if (Motherboard != null) total += Motherboard.BasePrice;
            if (Rams != null) 
                total += Rams.Sum(r => r.BasePrice);
            return total;
        }

        public string GetDescription()
        {
            var parts = new List<string>();
            if (Cpu != null) parts.Add($"CPU: {Cpu.Name}");
            if (Gpu != null) parts.Add($"GPU: {Gpu.Name}");
            if (Motherboard != null) parts.Add($"Motherboard: {Motherboard.Name}");
            return parts.Any() ? string.Join(", ", parts) + "\n" : "Пустая сборка";
        }

    }
}
