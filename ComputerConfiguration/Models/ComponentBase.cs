using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Models
{
    public abstract class ComponentBase : IComponent
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public double BasePrice { get; set; }
        public double Rating { get; set; }
        public bool InStock { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; } = new();

        public virtual double GetFinalPrice() => BasePrice;
    }
}
