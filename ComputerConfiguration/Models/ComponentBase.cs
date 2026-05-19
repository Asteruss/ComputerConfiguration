using ComputerConfiguration.Models.Enums;

namespace ComputerConfiguration.Models;

public abstract class ComponentBase : ComponentDTO, IComponent
{
    public int Id { get; set; } 
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public double BasePrice { get; set; }
    public double Rating { get; set; }
    public bool InStock { get; set; }
    public string Description { get; set; }
    public byte[] ImageData { get; set; }
    public ComponentCategory ComponentCategory { get; set; }
    public List<string> Tags { get; set; } = new();

}
