using ComputerConfiguration.Converters;
using ComputerConfiguration.DTO;
using ComputerConfiguration.Mappers;
using ComputerConfiguration.Models;
using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Components;
using ComputerConfiguration.Models.Enums;
using System.Collections.ObjectModel;

namespace ComputerConfiguration.Builder;

public class ComputerBuildSession : IComputerBuildSession
{
    private readonly IComputerBuildDtoBuilder _builder;
    private readonly IComputerBuildMapper _mapper;
    public event Action? DtoChanged;
    private ComputerBuildDTO CurrentDto => _builder.BuildDto();
    public ComputerBuildSession(IComputerBuildDtoBuilder builder, IComputerBuildMapper mapper)
    {
        _builder = builder;
        _mapper = mapper;
    }
    public event Action? SelectedServicesChanged;
    private void OnSelectedServicesChanged() => SelectedServicesChanged?.Invoke();

    public void SetCpu(Cpu cpu, ComponentStatus status = ComponentStatus.Selected)
    {
        if (CurrentDto.Cpu != null)
            RemoveCpu();

        cpu.Count--;
        cpu.ComponentStatus = status;
        _builder.WithCpu(cpu);
    }

    public void RemoveCpu()
    {
        if (CurrentDto.Cpu == null) return;
        CurrentDto.Cpu.Count++;
        CurrentDto.Cpu.ComponentStatus = ComponentStatus.NotSelected;
        _builder.WithCpu(null!);
    }

    public void SetGpu(Gpu gpu, ComponentStatus status = ComponentStatus.Selected)
    {
        if (CurrentDto.Gpu != null)
            CurrentDto.Gpu.ComponentStatus = ComponentStatus.NotSelected;

        gpu.Count--;
        gpu.ComponentStatus = status;
        _builder.WithGpu(gpu);
    }

    public void RemoveGpu()
    {
        if (CurrentDto.Gpu == null) return;
        CurrentDto.Gpu.Count++;
        CurrentDto.Gpu.ComponentStatus = ComponentStatus.NotSelected;
        _builder.WithGpu(null!);
    }

    public void SetMotherboard(Motherboard mb, ComponentStatus status = ComponentStatus.Selected)
    {
        if (CurrentDto.Motherboard != null)
            CurrentDto.Motherboard.ComponentStatus = ComponentStatus.NotSelected;

        mb.Count--;
        mb.ComponentStatus = status;
        _builder.WithMotherboard(mb);
    }

    public void RemoveMotherboard()
    {
        if (CurrentDto.Motherboard == null) return;
        CurrentDto.Motherboard.Count++;
        CurrentDto.Motherboard.ComponentStatus = ComponentStatus.NotSelected;
        _builder.WithMotherboard(null!);
    }

    public void SetCase(Case caseComponent, ComponentStatus status = ComponentStatus.Selected)
    {
        if (CurrentDto.Case != null)
            CurrentDto.Case.ComponentStatus = ComponentStatus.NotSelected;

        caseComponent.Count--;
        caseComponent.ComponentStatus = status;
        _builder.WithCase(caseComponent);
    }

    public void RemoveCase()
    {
        if (CurrentDto.Case == null) return;
        CurrentDto.Case.Count++;
        CurrentDto.Case.ComponentStatus = ComponentStatus.NotSelected;
        _builder.WithCase(null!);
    }

    public void SetCooler(Cooler cooler, ComponentStatus status = ComponentStatus.Selected)
    {
        if (CurrentDto.Cooler != null)
            CurrentDto.Cooler.ComponentStatus = ComponentStatus.NotSelected;

        cooler.Count--;
        cooler.ComponentStatus = status;
        _builder.WithCooler(cooler);
    }

    public void RemoveCooler()
    {
        if (CurrentDto.Cooler == null) return;
        CurrentDto.Cooler.Count++;
        CurrentDto.Cooler.ComponentStatus = ComponentStatus.NotSelected;
        _builder.WithCooler(null!);
    }

    public void SetPsu(Psu psu, ComponentStatus status = ComponentStatus.Selected)
    {
        if (CurrentDto.Psu != null)
            CurrentDto.Psu.ComponentStatus = ComponentStatus.NotSelected;

        psu.Count--;
        psu.ComponentStatus = status;
        _builder.WithPsu(psu);
    }

    public void RemovePsu()
    {
        if (CurrentDto.Psu == null) return;
        CurrentDto.Psu.Count++;
        CurrentDto.Psu.ComponentStatus = ComponentStatus.NotSelected;
        _builder.WithPsu(null!);
    }

    public void AddRam(Ram ram, ComponentStatus status = ComponentStatus.SelectedMany)
    {
        if (status == ComponentStatus.SelectedMany)
            ram.Count--;
        Ram copy = ram.ShallowCopy();
        copy.ComponentStatus = status;
        ram.ComponentStatus = status;
        if (status == ComponentStatus.SelectedMany)
            ram.CountSelected++;
        else if (status == ComponentStatus.SelectedManyAsFake)
            ram.CountFakeSelected++;

        _builder.AddRam(copy, ram);
    }

    public void RemoveRam(BuildRam ram)
    {
        ram.ComponentStatus = ComponentStatus.NotSelected;
        CurrentDto.Rams.Remove(ram);
    }

    public void ClearRams(Ram ram, ComponentStatus status = ComponentStatus.SelectedMany)
    {
        var toRemove = CurrentDto.Rams
            .Where(r => r.ComponentStatus == status && r.RamId == ram.Id)
            .ToList();

        foreach (var r in toRemove)
            RemoveRam(r);

        if (!CurrentDto.Rams.Any(r => r.RamId == ram.Id))
            ram.ComponentStatus = ComponentStatus.NotSelected;

        if (status == ComponentStatus.SelectedMany)
        {
            ram.Count += ram.CountSelected;
            ram.CountSelected = 0;
        }
        else if (status == ComponentStatus.SelectedManyAsFake)
            ram.CountFakeSelected = 0;
    }

    public void AddStorage(Storage storage, ComponentStatus status = ComponentStatus.SelectedMany)
    {
        if (status == ComponentStatus.SelectedMany)
            storage.Count--;
        var copy = storage.ShallowCopy();
        copy.ComponentStatus = status;
        storage.ComponentStatus = status;

        if (status == ComponentStatus.SelectedMany)
            storage.CountSelected++;
        else if (status == ComponentStatus.SelectedManyAsFake)
            storage.CountFakeSelected++;

        _builder.AddStorage(copy, storage);
    }

    public void RemoveStorage(BuildStorage storage)
    {
        storage.ComponentStatus = ComponentStatus.NotSelected;
        CurrentDto.Storages.Remove(storage);
    }

    public void ClearStorages(Storage storage, ComponentStatus status = ComponentStatus.SelectedMany)
    {
        var toRemove = CurrentDto.Storages
            .Where(s => s.ComponentStatus == status && s.StorageId == storage.Id)
            .ToList();

        foreach (var s in toRemove)
            RemoveStorage(s);

        if (!CurrentDto.Storages.Any(s => s.StorageId == storage.Id))
            storage.ComponentStatus = ComponentStatus.NotSelected;

        if (status == ComponentStatus.SelectedMany)
        {
            storage.Count += storage.CountSelected;
            storage.CountSelected = 0;
        }
        else if (status == ComponentStatus.SelectedManyAsFake)
            storage.CountFakeSelected = 0;
    }

    public void AddAdditionalOption(AdditionalServiceOption option)
    {
        _builder.AddAdditionalService(option);
        OnSelectedServicesChanged();

    }

    public void RemoveAdditionalOption(AdditionalServiceOption option)
    {
        if (!CurrentDto.SelectedAdditionalServices.Contains(option)) return;
        CurrentDto.SelectedAdditionalServices.Remove(option);
        OnSelectedServicesChanged();

    }

    public void RemoveAdditionalOptions(AdditionalService service)
    {
        var toRemove = CurrentDto.SelectedAdditionalServices
            .Where(o => o.AdditionalServiceId == service.Id)
            .ToList();

        if (toRemove.Count == 0) return;

        foreach (var opt in toRemove)
            CurrentDto.SelectedAdditionalServices.Remove(opt);

        OnSelectedServicesChanged();

    }

    public void ClearAdditionalServices()
    {
        CurrentDto.SelectedAdditionalServices.Clear();
        OnSelectedServicesChanged();
    }

    public ObservableCollection<AdditionalServiceOption> GetSelectedAdditionalServices()
        => CurrentDto.SelectedAdditionalServices.ToObservableCollection();

    public ComputerBuildDTO GetDto() => _builder.BuildDto();
    public ComputerBuild GetBuild() => _mapper.Map(GetDto());

    public void Reset()
    {
        _builder.Reset();
        DtoChanged?.Invoke();
    }  
}