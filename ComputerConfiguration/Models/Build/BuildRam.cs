using ComputerConfiguration.Models.Enums;
using ComputerConfiguration.Models.Components;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerConfiguration.Models.Build;

public class BuildRam
{
    public int Id { get; set; }
    public int? ComputerBuildId { get; set; }
    public ComputerBuild? Build { get; set; }
    public int? RamId { get; set; }
    public Ram? Ram { get; set; }
    [NotMapped]
    public Ram? RamHelper { get; set; }
    [NotMapped]
    public ComponentStatus ComponentStatus { get; set; }
}
