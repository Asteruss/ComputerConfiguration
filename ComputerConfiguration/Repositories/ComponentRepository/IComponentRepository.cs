using ComputerConfiguration.Models.Components;
namespace ComputerConfiguration.Repositories.ComponentRepository;

public interface IComponentRepository
{
    IEnumerable<Cpu> GetCpus();
    IEnumerable<Gpu> GetGpus();
    IEnumerable<Motherboard> GetMotherboards();
    IEnumerable<Ram> GetRam();
    IEnumerable<Psu> GetPsu();
    IEnumerable<Cooler> GetCoolers();
    IEnumerable<Case> GetCases();
    IEnumerable<Storage> GetStorages();
}
