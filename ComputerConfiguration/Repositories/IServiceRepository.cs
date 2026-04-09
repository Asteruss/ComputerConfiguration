using ComputerConfiguration.Models.Build;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Repositories
{
    public interface IServiceRepository
    {
        //IEnumerable<ServiceType> GetServiceTypes();
        IEnumerable<AdditionalService> GetAdditionalServices();
    }
}
