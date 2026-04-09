using ComputerConfiguration.DTO;
using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public void SetCpu(Cpu cpu) => _dto.Cpu = cpu;
    public void SetGpu(Gpu gpu) => _dto.Gpu = gpu;
    public void SetMotherboard(Motherboard motherboard) => _dto.Motherboard = motherboard;
    public void SetCase(Case caseComponent) => _dto.Case = caseComponent;
    public void SetCooler(Cooler cooler) => _dto.Cooler = cooler;
    public void SetPsu(Psu psu) => _dto.Psu = psu;

    public void AddRam(Ram ram) => _dto.Rams.Add(ram);
    public void RemoveRam(Ram ram) => _dto.Rams.Remove(ram);
    public void ClearRams() => _dto.Rams.Clear();

    public void AddStorage(Storage storage) => _dto.Storages.Add(storage);
    public void RemoveStorage(Storage storage) => _dto.Storages.Remove(storage);
    public void ClearStorages() => _dto.Storages.Clear();

    public void AddAdditionalService(AdditionalService service) => _dto.SelectedAdditionalServices.Add(service);
    public void RemoveAdditionalService(AdditionalService service) => _dto.SelectedAdditionalServices.Remove(service);
    public void ClearAdditionalServices() => _dto.SelectedAdditionalServices.Clear();

    public ComputerBuildDTO BuildDto()
    {
        return _dto;
    }

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
            Rams = _dto.Rams?.ToList() ?? new List<Ram>(),
            Storages = _dto.Storages?.ToList() ?? new List<Storage>(),
            AdditionalServices = _dto.SelectedAdditionalServices?.ToList() ?? []
        };
        return build;
    }
}
