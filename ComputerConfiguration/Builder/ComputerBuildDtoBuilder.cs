using ComputerConfiguration.DTO;
using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Components;

namespace ComputerConfiguration.Builder;

public class ComputerBuildDtoBuilder : IComputerBuildDtoBuilder
{
    private ComputerBuildDTO _dto;

    public ComputerBuildDtoBuilder() => Reset();

    public IComputerBuildDtoBuilder Reset()
    {
        _dto = new ComputerBuildDTO();
        return this;
    }

    public IComputerBuildDtoBuilder WithCpu(Cpu cpu)
    {
        _dto.Cpu = cpu;
        return this;
    }

    public IComputerBuildDtoBuilder WithGpu(Gpu gpu)
    {
        _dto.Gpu = gpu;
        return this;
    }

    public IComputerBuildDtoBuilder WithMotherboard(Motherboard motherboard)
    {
        _dto.Motherboard = motherboard;
        return this;
    }

    public IComputerBuildDtoBuilder WithCase(Case caseComponent)
    {
        _dto.Case = caseComponent;
        return this;
    }

    public IComputerBuildDtoBuilder WithCooler(Cooler cooler)
    {
        _dto.Cooler = cooler;
        return this;
    }

    public IComputerBuildDtoBuilder WithPsu(Psu psu)
    {
        _dto.Psu = psu;
        return this;
    }

    public IComputerBuildDtoBuilder AddRam(Ram ram, Ram ramReal)
    {
        _dto.Rams.Add(new BuildRam
        {
            RamId = ram.Id,
            ComponentStatus = ram.ComponentStatus,
            RamHelper = ramReal
        });
        return this;
    }

    public IComputerBuildDtoBuilder AddStorage(Storage storage, Storage storageReal)
    {
        _dto.Storages.Add(new BuildStorage
        {
            StorageId = storage.Id,
            ComponentStatus = storage.ComponentStatus,
            StorageHelper = storageReal
        });
        return this;
    }

    public IComputerBuildDtoBuilder AddAdditionalService(AdditionalServiceOption option)
    {
        _dto.SelectedAdditionalServices.Add(option);
        return this;
    }

    public ComputerBuildDTO BuildDto() => _dto;
}
