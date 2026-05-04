using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Enums;

namespace ComputerConfiguration.Repositories.ServicesRepository
{
    public class InMemoryServiceRepository : IServiceRepository
    {
        public IEnumerable<AdditionalService> GetAdditionalServices()
        {
            var osService = _getOsService();
            var warranyService = _getWarrantyYearsService();
            var cableService = _getCableManagementService();


            return [osService, warranyService, cableService];
        }

        private AdditionalService _getWarrantyYearsService()
        {
            var years = new List<AdditionalServiceOption>()
            {
                new AdditionalServiceOption{
                    Id = 6,
                    Option = "1",
                    AdditionalPrice = 2000
                },
                new AdditionalServiceOption{
                    Id = 7,
                    Option = "2",
                    AdditionalPrice = 1500
                },
                new AdditionalServiceOption{
                    Id = 8,
                    Option = "3",
                    AdditionalPrice = 1000
                },
            };
            var warrantyService = new AdditionalService
            {
                Id = 2,
                Name = "Гарантия",
                Description = "",
                AdditionalServiceOptions = years,
                OptionType = OptionType.MultiOption
            };

            foreach (var opt in years)
            {
                opt.AdditionalServiceId = warrantyService.Id;
                opt.AdditionalService = warrantyService;
            }
            return warrantyService;
        }

        private AdditionalService _getOsService()
        {
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
                AdditionalServiceOptions = osOptions,
                OptionType = OptionType.ListOption
            };

            foreach (var opt in osOptions)
            {
                opt.AdditionalServiceId = osService.Id;
                opt.AdditionalService = osService;
            }
            return osService;
        }

        private AdditionalService _getCableManagementService()
        {
            var cableOptions = new List<AdditionalServiceOption>()
            {
                new AdditionalServiceOption
                {
                    Id = 9,
                    Option = "Стандартная",
                    AdditionalPrice = 0
                },
                new AdditionalServiceOption
                {
                    Id = 10,
                    Option = "Черные кабели",
                    AdditionalPrice = 300
                },
                new AdditionalServiceOption
                {
                    Id = 11,
                    Option = "Полная кастомизация",
                    AdditionalPrice = 500
                }
            };
            var cableService = new AdditionalService
            {
                Id = 3,
                Name = "Менеджмент кабелей",
                Description = "",
                AdditionalServiceOptions = cableOptions,
                OptionType = OptionType.MultiOption
            };

            foreach (var opt in cableOptions)
            {
                opt.AdditionalServiceId = cableService.Id;
                opt.AdditionalService = cableService;
            }
            return cableService;
        }
    }
}
