using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Models.Build
{
    public class AdditionalService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ServiceTypeId { get; set; }
        public string Description { get; set; }
        public ServiceType? ServiceType { get; set; }
        public List<AdditionalServiceOption>? AdditionalServiceOptions { get; set; }
        public List<ComputerBuild>? ComputerBuilds { get; set; }
    }
}
