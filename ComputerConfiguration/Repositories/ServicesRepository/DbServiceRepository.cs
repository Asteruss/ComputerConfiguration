using ComputerConfiguration.DB;
using ComputerConfiguration.Models.Build;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Repositories.ServicesRepository
{
    public class DbServiceRepository : IServiceRepository
    {
        private readonly ComputerConfigurationDBContext _db;
        public DbServiceRepository(ComputerConfigurationDBContext db)
        {
            _db = db;
        }
        public IEnumerable<AdditionalService> GetAdditionalServices() => 
            _db.AdditionalServices.Include(a => a.AdditionalServiceOptions).ToList();
    }
}
