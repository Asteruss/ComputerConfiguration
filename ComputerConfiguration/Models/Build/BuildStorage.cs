using ComputerConfiguration.Models.Components;
using ComputerConfiguration.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerConfiguration.Models.Build;

public class BuildStorage
{
    public int Id { get; set; }
    public int? ComputerBuildId { get; set; }
    public ComputerBuild? Build { get; set; }
    public int? StorageId { get; set; }
    public Storage? Storage { get; set; }
    [NotMapped]
    public Storage? StorageHelper { get; set; }
    [NotMapped]

    public ComponentStatus ComponentStatus { get; set; }
}
