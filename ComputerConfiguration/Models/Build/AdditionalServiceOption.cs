using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Models.Build;

public class AdditionalServiceOption
{
    public int Id { get; set; }
    public string Option { get; set; }
    public double AdditionalPrice { get; set; }
    public int? AdditionalServiceId { get; set; }
    public AdditionalService? AdditionalService { get; set; }
    public List<ComputerBuild>? ComputerBuilds { get; set; } = new();


}
