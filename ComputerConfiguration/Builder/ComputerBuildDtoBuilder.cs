using ComputerConfiguration.Converters;
using ComputerConfiguration.DTO;
using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Components;
using ComputerConfiguration.Models.Enums;
using System.Collections.ObjectModel;
using System.Runtime.Intrinsics.Arm;
namespace ComputerConfiguration.Builder;

public class ComputerBuildDtoBuilder : IComputerBuildDtoBuilder
{
    private ComputerBuildDTO _dto;

    public ComputerBuildDtoBuilder()
    {
        Reset();
    }

    public void Reset()
    {
        _dto = new ComputerBuildDTO();
    }

    public void SetCpu(Cpu cpu, ComponentStatus status = ComponentStatus.Selected)
    {
        if (_dto.Cpu != null)
            _dto.Cpu.ComponentStatus = ComponentStatus.NotSelected;
        _dto.Cpu = cpu;
        _dto.Cpu.ComponentStatus = status;
    }


    public void RemoveCpu(Cpu cpu)
    {
        if (_dto.Cpu != null)
        {
            _dto.Cpu.ComponentStatus = ComponentStatus.NotSelected;
            _dto.Cpu = null;
        }
    }

    public void SetGpu(Gpu gpu, ComponentStatus status = ComponentStatus.Selected)
    {
        if (_dto.Gpu != null)
            _dto.Gpu.ComponentStatus = ComponentStatus.NotSelected;
        _dto.Gpu = gpu;
        _dto.Gpu.ComponentStatus = status;
    }

    public void RemoveGpu(Gpu gpu)
    {
        if (_dto.Gpu != null)
        {
            _dto.Gpu.ComponentStatus = ComponentStatus.NotSelected;
            _dto.Gpu = null;
        }
    }

    public void SetMotherboard(Motherboard motherboard, ComponentStatus status = ComponentStatus.Selected)
    {
        if (_dto.Motherboard != null)
            _dto.Motherboard.ComponentStatus = ComponentStatus.NotSelected;
        _dto.Motherboard = motherboard;
        _dto.Motherboard.ComponentStatus = status;
    }


    public void RemoveMotherboard(Motherboard motherboard)
    {
        if (_dto.Motherboard != null)
        {
            _dto.Motherboard.ComponentStatus = ComponentStatus.NotSelected;
            _dto.Motherboard = null;
        }
    }


    public void SetCase(Case caseComponent, ComponentStatus status = ComponentStatus.Selected)
    {
        if (_dto.Case != null)
            _dto.Case.ComponentStatus = ComponentStatus.NotSelected;
        _dto.Case = caseComponent;
        _dto.Case.ComponentStatus = status;
    }

    public void RemoveCase(Case caseComponent)
    {
        if (_dto.Case != null)
        {
            _dto.Case.ComponentStatus = ComponentStatus.NotSelected;
            _dto.Case = null;
        }
    }
    public void SetCooler(Cooler cooler, ComponentStatus status = ComponentStatus.Selected)
    {
        if (_dto.Cooler != null)
            _dto.Cooler.ComponentStatus = ComponentStatus.NotSelected;
        _dto.Cooler = cooler;
        _dto.Cooler.ComponentStatus = status;
    }


    public void RemoveCooler(Cooler cooler)
    {
        if (_dto.Cooler != null)
        {
            _dto.Cooler.ComponentStatus = ComponentStatus.NotSelected;
            _dto.Cooler = null;
        }
    }

    public void SetPsu(Psu psu, ComponentStatus status = ComponentStatus.Selected)
    {
        if (_dto.Psu != null)
            _dto.Psu.ComponentStatus = ComponentStatus.NotSelected;
        _dto.Psu = psu;
        _dto.Psu.ComponentStatus = status;
    }

    public void RemovePsu(Psu psu)
    {
        if (_dto.Psu != null)
        {
            _dto.Psu.ComponentStatus = ComponentStatus.NotSelected;
            _dto.Psu = null;
        }
    }
    public void AddRam(Ram ram, ComponentStatus status = ComponentStatus.SelectedMany)
    {
        Ram ramCopy = ram.ShallowCopy();
        ramCopy.ComponentStatus = status;
        ram.ComponentStatus = status;
        if (status == ComponentStatus.SelectedMany)
            ram.CountSelected++;
        else if (status == ComponentStatus.SelectedManyAsFake)
            ram.CountFakeSelected++;
        _dto.Rams.Add(ramCopy);
    }
    public void RemoveRam(Ram ram)
    {
        ram.ComponentStatus = ComponentStatus.NotSelected;
        _dto.Rams.Remove(ram);
    }
    public void ClearRams(Ram ramReal, ComponentStatus status = ComponentStatus.SelectedMany)
    {
        var rams = new List<Ram>(_dto.Rams);
        foreach (var ram in rams)
            if (ram.ComponentStatus == status && ram.Id == ramReal.Id)
                RemoveRam(ram);
        if (!_dto.Rams.Where(r => r.Id == ramReal.Id).Any())
            ramReal.ComponentStatus = ComponentStatus.NotSelected;
        if (status == ComponentStatus.SelectedMany)
            ramReal.CountSelected = 0;
        else if (status == ComponentStatus.SelectedManyAsFake)
            ramReal.CountFakeSelected = 0;
    }

    public void AddStorage(Storage storage, ComponentStatus status = ComponentStatus.SelectedMany)
    {
        var storageCopy = storage.ShallowCopy();
        storageCopy.ComponentStatus = status;
        storage.ComponentStatus = status;
        if (status == ComponentStatus.SelectedMany)
            storage.CountSelected++;
        else if (status == ComponentStatus.SelectedManyAsFake)
            storage.CountFakeSelected++;
        _dto.Storages.Add(storageCopy);
    }

    public void RemoveStorage(Storage storage)
    {
        storage.ComponentStatus = ComponentStatus.NotSelected;
        _dto.Storages.Remove(storage);
    }

    public void ClearStorages(Storage realStorage, ComponentStatus status = ComponentStatus.SelectedMany)
    {
        var storages = new List<Storage>(_dto.Storages);
        foreach (var storage in storages)
            if (storage.ComponentStatus == status && storage.Id == realStorage.Id)
                RemoveStorage(storage);
        if (!_dto.Storages.Where(s => s.Id == realStorage.Id).Any())
            realStorage.ComponentStatus = ComponentStatus.NotSelected;
        if (status == ComponentStatus.SelectedMany)
            realStorage.CountSelected = 0;
        else if (status == ComponentStatus.SelectedManyAsFake)
            realStorage.CountFakeSelected = 0;
    }

    public event Action? SelectedServicesChanged;
    private void OnSelectedServicesChanged() => SelectedServicesChanged?.Invoke();
    public void AddAdditionalOption(AdditionalServiceOption option)
    {
        _dto.SelectedAdditionalServices.Add(option);
        OnSelectedServicesChanged();
    }

    public void RemoveAdditionalOption(AdditionalServiceOption option)
    {
        if (_dto.SelectedAdditionalServices.Contains(option))
        {
            _dto.SelectedAdditionalServices.Remove(option);
            OnSelectedServicesChanged();
        }
    }

    public void RemoveAdditionalOptions(AdditionalService service)
    {
        var toRemove = _dto.SelectedAdditionalServices
            .Where(o => o.AdditionalServiceId == service.Id)
            .ToList();
        foreach (var opt in toRemove)
            _dto.SelectedAdditionalServices.Remove(opt);

        if (toRemove.Any())
            OnSelectedServicesChanged();
    }

    public void ClearAdditionalServices() => _dto.SelectedAdditionalServices.Clear();
    public ObservableCollection<AdditionalServiceOption> GetSelectedAdditionalServices() => _dto.SelectedAdditionalServices.ToObservableCollection();

    public ComputerBuildDTO BuildDto() => _dto;

    public ComputerBuild BuildFinal()
    {
        var build = new ComputerBuild
        {
            Cpu = _dto.Cpu,
            Gpu = _dto.Gpu,
            Motherboard = _dto.Motherboard,
            Case = _dto.Case,
            Cooler = _dto.Cooler,
            Psu = _dto.Psu,
            Rams = _dto.Rams?.ToList() ?? [],
            Storages = _dto.Storages?.ToList() ?? [],
            AdditionalServices = _dto.SelectedAdditionalServices?.ToList() ?? []
        };
        return build;
    }
}
