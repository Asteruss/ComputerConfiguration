using ComputerConfiguration.Models;
using ComputerConfiguration.Models.Enums;


namespace ComputerConfiguration.Filters;

public interface IComponentFilterProvider
{
    IEnumerable<FilterBase> GetFilters(ComponentCategory type, IEnumerable<IComponent> allComponents);
}
