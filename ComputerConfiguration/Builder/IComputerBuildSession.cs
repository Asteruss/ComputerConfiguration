using ComputerConfiguration.DTO;
using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Components;
using ComputerConfiguration.Models.Enums;
using System.Collections.ObjectModel;

namespace ComputerConfiguration.Builder;

public interface IComputerBuildSession
{
    event Action SelectedServicesChanged;

    void SetCpu(Cpu cpu, ComponentStatus status = ComponentStatus.Selected);
    void RemoveCpu();

    void SetGpu(Gpu gpu, ComponentStatus status = ComponentStatus.Selected);
    void RemoveGpu();

    void SetMotherboard(Motherboard mb, ComponentStatus status = ComponentStatus.Selected);
    void RemoveMotherboard();

    void SetCase(Case caseComponent, ComponentStatus status = ComponentStatus.Selected);
    void RemoveCase();

    void SetCooler(Cooler cooler, ComponentStatus status = ComponentStatus.Selected);
    void RemoveCooler();

    void SetPsu(Psu psu, ComponentStatus status = ComponentStatus.Selected);
    void RemovePsu();

    void AddRam(Ram ram, ComponentStatus status = ComponentStatus.SelectedMany);
    void RemoveRam(BuildRam ram);
    void ClearRams(Ram ram, ComponentStatus status = ComponentStatus.SelectedMany);

    void AddStorage(Storage storage, ComponentStatus status = ComponentStatus.SelectedMany);
    void RemoveStorage(BuildStorage storage);
    void ClearStorages(Storage storage, ComponentStatus status = ComponentStatus.SelectedMany);

    void AddAdditionalOption(AdditionalServiceOption option);
    void RemoveAdditionalOption(AdditionalServiceOption option);
    void RemoveAdditionalOptions(AdditionalService service);
    void ClearAdditionalServices();

    ObservableCollection<AdditionalServiceOption> GetSelectedAdditionalServices();
    ComputerBuildDTO GetDto();
    ComputerBuild GetBuild();
    void Reset();
}