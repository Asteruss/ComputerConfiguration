using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Models
{
    public interface IComponent
    {
        int Id { get; }
        string Name { get; set; }
        string Manufacturer { get; set; }
        double BasePrice { get; set; }
        double Rating { get; set; }
        List<string> Tags { get; set; }
        bool InStock { get; set; }
    }
}
