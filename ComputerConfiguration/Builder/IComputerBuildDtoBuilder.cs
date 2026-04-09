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
    public interface IComputerBuildDtoBuilder
    {
        void SetCpu(Cpu cpu);
        void SetGpu(Gpu gpu);
        void SetMotherboard(Motherboard motherboard);
        void AddRam(Ram ram);
        ComputerBuildDTO BuildDto();         
        ComputerBuild BuildFinal();           
    }
}
