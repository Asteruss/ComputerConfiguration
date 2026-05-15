using ComputerConfiguration.Models.Enums;

namespace ComputerConfiguration.Models;

public interface IComponent
{
    int Id { get; }
    string Name { get; set; }
    string Manufacturer { get; set; }
    double BasePrice { get; set; }
    double Rating { get; set; }
    List<string> Tags { get; set; }
    bool InStock { get; set; }
    byte[] ImageData { get; set; }
    ComponentCategory ComponentCategory { get; set; }


}
