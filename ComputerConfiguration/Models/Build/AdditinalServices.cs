using ComputerConfiguration.Models.Enums;

namespace ComputerConfiguration.Models.Build;

public class AdditionalService
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public OptionType OptionType { get; set; }
    public List<AdditionalServiceOption>? AdditionalServiceOptions { get; set; }
    public List<ComputerBuild>? ComputerBuilds { get; set; }
}
