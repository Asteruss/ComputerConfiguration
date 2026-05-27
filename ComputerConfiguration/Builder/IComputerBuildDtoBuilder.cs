using ComputerConfiguration.DTO;
using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Components;
namespace ComputerConfiguration.Builder;

public interface IComputerBuildDtoBuilder
{
    IComputerBuildDtoBuilder Reset();
    IComputerBuildDtoBuilder WithCpu(Cpu cpu);
    IComputerBuildDtoBuilder WithGpu(Gpu gpu);
    IComputerBuildDtoBuilder WithMotherboard(Motherboard motherboard);
    IComputerBuildDtoBuilder WithCase(Case caseComponent);
    IComputerBuildDtoBuilder WithCooler(Cooler cooler);
    IComputerBuildDtoBuilder WithPsu(Psu psu);
    IComputerBuildDtoBuilder AddRam(Ram ram, Ram ramReal);
    IComputerBuildDtoBuilder AddStorage(Storage storage, Storage storageReal);
    IComputerBuildDtoBuilder AddAdditionalService(AdditionalServiceOption option);
    ComputerBuildDTO BuildDto();
}