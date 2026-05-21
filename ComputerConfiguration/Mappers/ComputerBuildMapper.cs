using ComputerConfiguration.DTO;
using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Enums;


namespace ComputerConfiguration.Mappers;

public class ComputerBuildMapper : IComputerBuildMapper
{
    public ComputerBuild Map(ComputerBuildDTO dto) => new()
    {
        Cpu = dto.Cpu?.ComponentStatus == ComponentStatus.Selected ? dto.Cpu : null,
        Gpu = dto.Gpu?.ComponentStatus == ComponentStatus.Selected ? dto.Gpu : null,
        Motherboard = dto.Motherboard?.ComponentStatus == ComponentStatus.Selected ? dto.Motherboard : null,
        Case = dto.Case?.ComponentStatus == ComponentStatus.Selected ? dto.Case : null,
        Cooler = dto.Cooler?.ComponentStatus == ComponentStatus.Selected ? dto.Cooler : null,
        Psu = dto.Psu?.ComponentStatus == ComponentStatus.Selected ? dto.Psu : null,

        BuildRams = dto.Rams?.Where(r => r.ComponentStatus == ComponentStatus.SelectedMany).ToList() ?? [],
        BuildStorages = dto.Storages?.Where(s => s.ComponentStatus == ComponentStatus.SelectedMany).ToList() ?? [],

        AdditionalServices = dto.SelectedAdditionalServices?.ToList() ?? []
    };
}
