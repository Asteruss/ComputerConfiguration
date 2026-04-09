using ComputerConfiguration.DB;
using ComputerConfiguration.Models.Build;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ComputerConfigurationDBContext _db;
        public ServiceRepository(ComputerConfigurationDBContext db)
        {
            _db = db;
        }
        public IEnumerable<ServiceType> GetServiceTypes() => _db.ServiceTypes;
        public IEnumerable<AdditionalService> GetAdditionalServices() => 
            _db.AdditionalServices.Include(a => a.AdditionalServiceOptions).ToList();
    }
}
