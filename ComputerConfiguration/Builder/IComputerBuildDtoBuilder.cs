using ComputerConfiguration.DTO;
using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Components;
using System.Collections.ObjectModel;
namespace ComputerConfiguration.Builder;


public interface IComputerBuildDtoBuilder
{
    void Reset();
    void SetCpu(Cpu cpu);
    void RemoveCpu(Cpu cpu);
    void SetGpu(Gpu gpu);
    void RemoveGpu(Gpu gpu);
    void SetMotherboard(Motherboard motherboard);
    void RemoveMotherboard(Motherboard motherboard);
    void SetCase(Case caseComponent);
    void RemoveCase(Case caseComponent);
    void SetCooler(Cooler cooler);
    void RemoveCooler(Cooler cooler);
    void SetPsu(Psu psu);
    void RemovePsu(Psu psu);


    void AddRam(Ram ram);
    void RemoveRam(Ram ram);
    void ClearRams(Ram ramReal);
    void ClearFakeRams(Ram ramReal);

    void AddStorage(Storage storage);
    void RemoveStorage(Storage storage);
    void ClearStorages(Storage realStorage);
    void ClearFakeStorages(Storage realStorage);

    event Action? SelectedServicesChanged;
    void AddAdditionalOption(AdditionalServiceOption option);
    void RemoveAdditionalOption(AdditionalServiceOption option);
    void RemoveAdditionalOptions(AdditionalService service);
    void ClearAdditionalServices();
    public ObservableCollection<AdditionalServiceOption> GetSelectedAdditionalServices();

    void SetFakeCpu(Cpu cpu);
    void SetFakeGpu(Gpu gpu);
    void SetFakeMotherboard(Motherboard motherboard);
    void SetFakeCase(Case caseComponent);
    void SetFakeCooler(Cooler cooler);
    void SetFakePsu(Psu psu);
    void AddFakeRam(Ram ram);
    void AddFakeStorage(Storage storage);


    ComputerBuildDTO BuildDto();
    ComputerBuild BuildFinal();
}