using ComputerConfiguration.Models;
using ComputerConfiguration.Models.Enums;
using ComputerConfiguration.Repositories.ComponentRepository;

namespace ComputerConfiguration.Services.Components;

public class ComponentFactory : IComponentFactory
{
    private readonly IComponentRepository _componentRepository;
    public ComponentFactory(IComponentRepository componentRepository)
    {
        _componentRepository = componentRepository;
    }
    public IEnumerable<ComponentBase> GetComponent(ComponentCategory category) => category switch
    {
        ComponentCategory.CPU => _componentRepository.GetCpus(),
        ComponentCategory.GPU => _componentRepository.GetGpus(),
        ComponentCategory.Motherboard => _componentRepository.GetMotherboards(),
        ComponentCategory.RAM => _componentRepository.GetRam(),
        ComponentCategory.Storage => _componentRepository.GetStorages(),
        ComponentCategory.PSU => _componentRepository.GetPsu(),
        ComponentCategory.Case => _componentRepository.GetCases(),
        ComponentCategory.Cooler => _componentRepository.GetCoolers(),
        _ => throw new ArgumentOutOfRangeException(nameof(category), category, null)
    };
}
