using ComputerConfiguration.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Repositories
{
    public interface IComponentRepository
    {
        IEnumerable<Cpu> GetCpus();
        IEnumerable<Gpu> GetGpus();
        IEnumerable<Motherboard> GetMotherboards();
        IEnumerable<Ram> GetRam();
    }
}
