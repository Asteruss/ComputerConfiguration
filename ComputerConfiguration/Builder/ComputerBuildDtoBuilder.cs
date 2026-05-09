using ComputerConfiguration.Converters;
using ComputerConfiguration.DTO;
using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Components;
using ComputerConfiguration.Models.Enums;
using System.Collections.ObjectModel;
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

    public void SetCpu(Cpu cpu)
    {
        if (_dto.Cpu != null)
            _dto.Cpu.ComponentStatus = ComponentStatus.NotSelected;
        _dto.Cpu = cpu;
        _dto.Cpu.ComponentStatus = ComponentStatus.Selected;
    }

    public void SetFakeCpu(Cpu cpu)
    {
        if (_dto.Cpu != null)
            _dto.Cpu.ComponentStatus = ComponentStatus.NotSelected;
        _dto.Cpu = cpu;
        _dto.Cpu.ComponentStatus = ComponentStatus.SelectedAsFake;
    }

    public void RemoveCpu(Cpu cpu)
    {
        if (_dto.Cpu != null)
        {
            _dto.Cpu.ComponentStatus = ComponentStatus.NotSelected;
            _dto.Cpu = null;
        }
    }

    public void SetGpu(Gpu gpu)
    {
        if (_dto.Gpu != null)
            _dto.Gpu.ComponentStatus = ComponentStatus.NotSelected;
        _dto.Gpu = gpu;
        _dto.Gpu.ComponentStatus = ComponentStatus.Selected;
    }
    public void SetFakeGpu(Gpu gpu)
    {
        if (_dto.Gpu != null)
            _dto.Gpu.ComponentStatus = ComponentStatus.NotSelected;
        _dto.Gpu = gpu;
        _dto.Gpu.ComponentStatus = ComponentStatus.SelectedAsFake;
    }

    public void RemoveGpu(Gpu gpu)
    {
        if (_dto.Gpu != null)
        {
            _dto.Gpu.ComponentStatus = ComponentStatus.NotSelected;
            _dto.Gpu = null;
        }
    }

    public void SetMotherboard(Motherboard motherboard)
    {
        if (_dto.Motherboard != null)
            _dto.Motherboard.ComponentStatus = ComponentStatus.NotSelected;
        _dto.Motherboard = motherboard;
        _dto.Motherboard.ComponentStatus = ComponentStatus.Selected;
    }

    public void SetFakeMotherboard(Motherboard motherboard)
    {
        if (_dto.Motherboard != null)
            _dto.Motherboard.ComponentStatus = ComponentStatus.NotSelected;
        _dto.Motherboard = motherboard;
        _dto.Motherboard.ComponentStatus = ComponentStatus.SelectedAsFake;
    }

    public void RemoveMotherboard(Motherboard motherboard)
    {
        if (_dto.Motherboard != null)
        {
            _dto.Motherboard.ComponentStatus = ComponentStatus.NotSelected;
            _dto.Motherboard = null;
        }
    }


    public void SetCase(Case caseComponent)
    {
        if (_dto.Case != null)
            _dto.Case.ComponentStatus = ComponentStatus.NotSelected;
        _dto.Case = caseComponent;
        _dto.Case.ComponentStatus = ComponentStatus.Selected;
    }

    public void RemoveCase(Case caseComponent)
    {
        if (_dto.Case != null)
        {
            _dto.Case.ComponentStatus = ComponentStatus.NotSelected;
            _dto.Case = null;
        }
    }

    public void SetFakeCase(Case caseComponent)
    {
        if (_dto.Case != null)
            _dto.Case.ComponentStatus = ComponentStatus.NotSelected;
        _dto.Case = caseComponent;
        _dto.Case.ComponentStatus = ComponentStatus.SelectedAsFake;
    }

    public void SetCooler(Cooler cooler)
    {
        if (_dto.Cooler != null)
            _dto.Cooler.ComponentStatus = ComponentStatus.NotSelected;
        _dto.Cooler = cooler;
        _dto.Cooler.ComponentStatus = ComponentStatus.Selected;
    }

    public void SetFakeCooler(Cooler cooler)
    {
        if (_dto.Cooler != null)
            _dto.Cooler.ComponentStatus = ComponentStatus.NotSelected;
        _dto.Cooler = cooler;
        _dto.Cooler.ComponentStatus = ComponentStatus.SelectedAsFake;
    }

    public void RemoveCooler(Cooler cooler)
    {
        if (_dto.Cooler != null)
        {
            _dto.Cooler.ComponentStatus = ComponentStatus.NotSelected;
            _dto.Cooler = null;
        }
    }

    public void SetPsu(Psu psu)
    {
        if (_dto.Psu != null)
            _dto.Psu.ComponentStatus = ComponentStatus.NotSelected;
        _dto.Psu = psu;
        _dto.Psu.ComponentStatus = ComponentStatus.Selected;
    }

    public void SetFakePsu(Psu psu)
    {
        if (_dto.Psu != null)
            _dto.Psu.ComponentStatus = ComponentStatus.NotSelected;
        _dto.Psu = psu;
        _dto.Psu.ComponentStatus = ComponentStatus.SelectedAsFake;
    }
    public void RemovePsu(Psu psu)
    {
        if (_dto.Psu != null)
        {
            _dto.Psu.ComponentStatus = ComponentStatus.NotSelected;
            _dto.Psu = null;
        }
    }
    public void AddRam(Ram ram)
    {
        Ram ramCopy = ram.ShallowCopy();
        ramCopy.ComponentStatus = ComponentStatus.SelectedMany;
        ram.ComponentStatus = ComponentStatus.SelectedMany;
        _dto.Rams.Add(ramCopy);
    }
    public void AddFakeRam(Ram ram)
    {
        Ram ramCopy = ram.ShallowCopy();
        ramCopy.ComponentStatus = ComponentStatus.SelectedManyAsFake;
        ram.ComponentStatus = ComponentStatus.SelectedManyAsFake;
        _dto.Rams.Add(ramCopy);
    }
    public void RemoveRam(Ram ram)
    {
        ram.ComponentStatus = ComponentStatus.NotSelected;
        _dto.Rams.Remove(ram);
    }
    public void ClearRams(Ram ramReal)
    {
        var rams = new List<Ram>(_dto.Rams);
        foreach (var ram in rams)
            if (ram.ComponentStatus == ComponentStatus.SelectedMany && ram.Id == ramReal.Id)
                RemoveRam(ram);
        if (!_dto.Rams.Where(r => r.Id == ramReal.Id).Any())
            ramReal.ComponentStatus = ComponentStatus.NotSelected;
    }
    public void ClearFakeRams(Ram ramReal)
    {
        var rams = new List<Ram>(_dto.Rams);
        foreach (var ram in rams)
            if (ram.ComponentStatus == ComponentStatus.SelectedManyAsFake && ram.Id == ramReal.Id) 
                RemoveRam(ram);
        if (!_dto.Rams.Where(r => r.Id == ramReal.Id).Any())
            ramReal.ComponentStatus = ComponentStatus.NotSelected;
    }

    public void AddStorage(Storage storage)
    {
        var storageCopy = storage.ShallowCopy();
        storageCopy.ComponentStatus = ComponentStatus.SelectedMany;
        storage.ComponentStatus = ComponentStatus.SelectedMany;
        _dto.Storages.Add(storageCopy);
    }

    public void AddFakeStorage(Storage storage)
    {
        var storageCopy = storage.ShallowCopy();
        storageCopy.ComponentStatus = ComponentStatus.SelectedManyAsFake;
        storage.ComponentStatus = ComponentStatus.SelectedManyAsFake;
        _dto.Storages.Add(storageCopy);
    }

    public void RemoveStorage(Storage storage)
    {
        storage.ComponentStatus = ComponentStatus.NotSelected;
        _dto.Storages.Remove(storage);
    }

    public void ClearStorages(Storage realStorage)
    {
        var storages = new List<Storage>(_dto.Storages);
        foreach (var storage in storages)
            if (storage.ComponentStatus == ComponentStatus.SelectedMany && storage.Id == realStorage.Id)
                RemoveStorage(storage);
        if (!_dto.Storages.Where(s => s.Id == realStorage.Id).Any())
            realStorage.ComponentStatus = ComponentStatus.NotSelected;
    }

    public void ClearFakeStorages(Storage realStorage)
    {
        var storages = new List<Storage>(_dto.Storages);
        foreach (var storage in storages)
            if (storage.ComponentStatus == ComponentStatus.SelectedManyAsFake && storage.Id == realStorage.Id)
                RemoveStorage(storage);
        if (!_dto.Storages.Where(s => s.Id == realStorage.Id).Any())
            realStorage.ComponentStatus = ComponentStatus.NotSelected;

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
