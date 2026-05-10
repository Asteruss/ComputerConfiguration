using ComputerConfiguration.DTO;
using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Components;
using ComputerConfiguration.Models.Enums;
using System.Collections.ObjectModel;
namespace ComputerConfiguration.Builder;


public interface IComputerBuildDtoBuilder
{
    void Reset();
    void SetCpu(Cpu cpu, ComponentStatus status = ComponentStatus.Selected);
    void RemoveCpu(Cpu cpu);
    void SetGpu(Gpu gpu, ComponentStatus status = ComponentStatus.Selected);
    void RemoveGpu(Gpu gpu);
    void SetMotherboard(Motherboard motherboard, ComponentStatus status = ComponentStatus.Selected);
    void RemoveMotherboard(Motherboard motherboard);
    void SetCase(Case caseComponent, ComponentStatus status = ComponentStatus.Selected);
    void RemoveCase(Case caseComponent);
    void SetCooler(Cooler cooler, ComponentStatus status = ComponentStatus.Selected);
    void RemoveCooler(Cooler cooler);
    void SetPsu(Psu psu, ComponentStatus status = ComponentStatus.Selected);
    void RemovePsu(Psu psu);


    void AddRam(Ram ram, ComponentStatus status = ComponentStatus.SelectedMany);
    void RemoveRam(Ram ram);
    void ClearRams(Ram ramReal, ComponentStatus status = ComponentStatus.SelectedMany);

    void AddStorage(Storage storage, ComponentStatus status = ComponentStatus.SelectedMany);
    void RemoveStorage(Storage storage);
    void ClearStorages(Storage realStorage, ComponentStatus status = ComponentStatus.SelectedMany);

    event Action? SelectedServicesChanged;
    void AddAdditionalOption(AdditionalServiceOption option);
    void RemoveAdditionalOption(AdditionalServiceOption option);
    void RemoveAdditionalOptions(AdditionalService service);
    void ClearAdditionalServices();
    public ObservableCollection<AdditionalServiceOption> GetSelectedAdditionalServices();

    ComputerBuildDTO BuildDto();
    ComputerBuild BuildFinal();
}