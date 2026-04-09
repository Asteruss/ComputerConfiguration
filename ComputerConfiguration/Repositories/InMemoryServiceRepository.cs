using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerConfiguration.Models.Build;

namespace ComputerConfiguration.Repositories
{
    public class InMemoryServiceRepository : IServiceRepository
    {
        public IEnumerable<AdditionalService> GetAdditionalServices()
        {
            // Создаем опции для ОС
            var osOptions = new List<AdditionalServiceOption>
            {
                new AdditionalServiceOption
                {
                    Id = 1,
                    Option = "Windows 11 Домашняя",
                    AdditionalPrice = 5000
                },
                new AdditionalServiceOption
                {
                    Id = 2,
                    Option = "Windows 11 Pro",
                    AdditionalPrice = 8000
                },
                new AdditionalServiceOption
                {
                    Id = 3,
                    Option = "Windows 10 Домашняя",
                    AdditionalPrice = 4000
                },
                new AdditionalServiceOption
                {
                    Id = 4,
                    Option = "Ubuntu 22.04 LTS",
                    AdditionalPrice = 0
                },
                new AdditionalServiceOption
                {
                    Id = 5,
                    Option = "Fedora 38",
                    AdditionalPrice = 0
                }
            };

            var osService = new AdditionalService
            {
                Id = 1,
                Name = "Установка операционной системы",
                Description = "Установка выбранной ОС с настройкой драйверов",
                AdditionalServiceOptions = osOptions
            };

            foreach (var opt in osOptions)
            {
                opt.AdditionalServiceId = osService.Id;
                opt.AdditionalService = osService;
            }
            
            var osServiceType = new ServiceType
            {
                Id = 1,
                ServiceName = "Операционная система",
                AdditionalServices = new List<AdditionalService> { osService }
            };
            foreach (var opt in osOptions)
            {
                opt.ServiceType = osServiceType;
            }

            return [osService];
        }
    }
}
