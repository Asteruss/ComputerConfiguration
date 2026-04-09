using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Models.Build
{
    public class ServiceType
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public List<AdditionalService>? AdditionalServices { get; set; }
    }
}
