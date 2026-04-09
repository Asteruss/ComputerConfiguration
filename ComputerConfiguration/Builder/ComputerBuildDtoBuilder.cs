using ComputerConfiguration.DTO;
using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Builder
{
    public class ComputerBuildDtoBuilder : IComputerBuildDtoBuilder
    {
        private ComputerBuildDTO _dto = new();

        public void SetCpu(Cpu cpu) => _dto.Cpu = cpu;
        public void SetGpu(Gpu gpu) => _dto.Gpu = gpu;
        public void SetMotherboard(Motherboard motherboard) => _dto.Motherboard = motherboard;
        public void AddRam(Ram ram) => _dto.Rams.Add(ram);

        public ComputerBuildDTO BuildDto() => _dto;

        public ComputerBuild BuildFinal()
        {
            var build = new ComputerBuild
            {
                Cpu = _dto.Cpu,
                Gpu = _dto.Gpu,
                Motherboard = _dto.Motherboard,
                Rams = _dto.Rams ?? []
            };
            return build;
        }
    }
}
