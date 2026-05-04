using ComputerConfiguration.DTO;
using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Components;
namespace ComputerConfiguration.Builder;


public interface IComputerBuildDtoBuilder
{
    void Reset();
    void SetCpu(Cpu cpu);
    void SetGpu(Gpu gpu);
    void SetMotherboard(Motherboard motherboard);
    void SetCase(Case caseComponent);
    void SetCooler(Cooler cooler);
    void SetPsu(Psu psu);

    void AddRam(Ram ram);
    void RemoveRam(Ram ram);
    void ClearRams();

    void AddStorage(Storage storage);
    void RemoveStorage(Storage storage);
    void ClearStorages();

    void AddAdditionalService(AdditionalServiceOption service);
    void RemoveAdditionalService(AdditionalServiceOption service);
    void ClearAdditionalServices();

    ComputerBuildDTO BuildDto();
    ComputerBuild BuildFinal();
}