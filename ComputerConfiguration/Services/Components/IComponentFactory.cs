using ComputerConfiguration.Models;
using ComputerConfiguration.Models.Enums;
namespace ComputerConfiguration.Services.Components;

public interface IComponentFactory
{
    IEnumerable<ComponentBase> GetComponent(ComponentCategory category);
}
