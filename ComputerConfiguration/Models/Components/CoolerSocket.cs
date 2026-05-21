using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Models.Components;

public class CoolerSocket
{
    public int Id { get; set; }
    public int? CoolerId { get; set; }
    public Cooler Cooler { get; set; }
    public string Socket { get; set; }
}
