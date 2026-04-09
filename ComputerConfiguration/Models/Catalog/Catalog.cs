using ComputerConfiguration.Models.Components;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Models.Catalog
{
    public sealed class ComponentCatalog
    {
        public List<Cpu> Cpus { get; private set; } = new();
        public List<Motherboard> Motherboards { get; private set; } = new();
        public List<Ram> Rams { get; private set; } = new();
        public List<Gpu> Gpus { get; private set; } = new();
        public List<Storage> Storages { get; private set; } = new();
        public List<Psu> Psus { get; private set; } = new();
        public List<Case> Cases { get; private set; } = new();
        public List<Cooler> Coolers { get; private set; } = new();

        public ComponentCatalog()
        {
            GetCpu();
            GetGpu();
            GetMotherboards();
            GetRam();
        }

        public void GetRam()
        {
            Rams = new List<Ram>
    {
        new Ram
        {
            Name = "Corsair ValueSelect 4GB DDR3-1333",
            Manufacturer = "Corsair",
            MemoryType = "DDR3",
            Capacity = 4,
            ModuleCount = 1,
            Speed = 1333,
            Timing = "9-9-9-24",
            Voltage = 1.5,
            BasePrice = 20,
            Rating = 4.0,
            InStock = true,
            Description = "Надёжная планка для старых систем.",
            Tags = new List<string> { "budget", "DDR3" }
        },
        new Ram
        {
            Name = "Kingston HyperX Fury 8GB DDR3-1866",
            Manufacturer = "Kingston",
            MemoryType = "DDR3",
            Capacity = 8,
            ModuleCount = 1,
            Speed = 1866,
            Timing = "10-11-10-30",
            Voltage = 1.5,
            BasePrice = 35,
            Rating = 4.3,
            InStock = false,
            Description = "Игровая память для платформ DDR3.",
            Tags = new List<string> { "gaming", "DDR3" }
        },

        // DDR4 – одиночные модули
        new Ram
        {
            Name = "Crucial 8GB DDR4-2400",
            Manufacturer = "Crucial",
            MemoryType = "DDR4",
            Capacity = 8,
            ModuleCount = 1,
            Speed = 2400,
            Timing = "17-17-17-39",
            Voltage = 1.2,
            BasePrice = 30,
            Rating = 4.2,
            InStock = true,
            Description = "Базовая память для офисных ПК.",
            Tags = new List<string> { "budget", "DDR4" }
        },
        new Ram
        {
            Name = "Samsung 16GB DDR4-2666",
            Manufacturer = "Samsung",
            MemoryType = "DDR4",
            Capacity = 16,
            ModuleCount = 1,
            Speed = 2666,
            Timing = "19-19-19-43",
            Voltage = 1.2,
            BasePrice = 55,
            Rating = 4.5,
            InStock = true,
            Description = "Надёжная OEM-планка для серверов и рабочих станций.",
            Tags = new List<string> { "server", "DDR4" }
        },
        new Ram
        {
            Name = "Kingston Fury Beast 16GB DDR4-3200",
            Manufacturer = "Kingston",
            MemoryType = "DDR4",
            Capacity = 16,
            ModuleCount = 1,
            Speed = 3200,
            Timing = "16-18-18-36",
            Voltage = 1.35,
            BasePrice = 60,
            Rating = 4.6,
            InStock = true,
            Description = "Игровая память с радиатором и неплохими таймингами.",
            Tags = new List<string> { "gaming", "DDR4" }
        },
        new Ram
        {
            Name = "G.Skill Ripjaws V 32GB DDR4-3600",
            Manufacturer = "G.Skill",
            MemoryType = "DDR4",
            Capacity = 32,
            ModuleCount = 1,
            Speed = 3600,
            Timing = "18-22-22-42",
            Voltage = 1.35,
            BasePrice = 110,
            Rating = 4.7,
            InStock = true,
            Description = "Высокочастотная планка для производительных сборок.",
            Tags = new List<string> { "performance", "DDR4" }
        },

        // DDR4 – комплекты (две планки)
        new Ram
        {
            Name = "Corsair Vengeance LPX 16GB (2x8GB) DDR4-3200",
            Manufacturer = "Corsair",
            MemoryType = "DDR4",
            Capacity = 8,
            ModuleCount = 2,
            Speed = 3200,
            Timing = "16-18-18-36",
            Voltage = 1.35,
            BasePrice = 75,
            Rating = 4.8,
            InStock = true,
            Description = "Популярный комплект для двухканального режима.",
            Tags = new List<string> { "gaming", "kit", "DDR4" }
        },
        new Ram
        {
            Name = "TeamGroup T-Force Delta RGB 32GB (2x16GB) DDR4-3600",
            Manufacturer = "TeamGroup",
            MemoryType = "DDR4",
            Capacity = 16,
            ModuleCount = 2,
            Speed = 3600,
            Timing = "18-20-20-40",
            Voltage = 1.35,
            BasePrice = 130,
            Rating = 4.7,
            InStock = true,
            Description = "Яркая RGB-подсветка и высокая частота.",
            Tags = new List<string> { "gaming", "RGB", "kit", "DDR4" }
        },
        new Ram
        {
            Name = "Kingston Server Premier 64GB (2x32GB) DDR4-2666",
            Manufacturer = "Kingston",
            MemoryType = "DDR4",
            Capacity = 32,
            ModuleCount = 2,
            Speed = 2666,
            Timing = "19-19-19-43",
            Voltage = 1.2,
            BasePrice = 250,
            Rating = 4.8,
            InStock = false,
            Description = "Серверная память с регистром (ECC? но для простоты не указываем).",
            Tags = new List<string> { "server", "kit", "DDR4" }
        },

        // DDR5 – одиночные
        new Ram
        {
            Name = "Crucial 16GB DDR5-4800",
            Manufacturer = "Crucial",
            MemoryType = "DDR5",
            Capacity = 16,
            ModuleCount = 1,
            Speed = 4800,
            Timing = "40-39-39-77",
            Voltage = 1.1,
            BasePrice = 70,
            Rating = 4.4,
            InStock = true,
            Description = "Базовая DDR5 для новых платформ.",
            Tags = new List<string> { "budget", "DDR5" }
        },
        new Ram
        {
            Name = "G.Skill Trident Z5 32GB DDR5-6000",
            Manufacturer = "G.Skill",
            MemoryType = "DDR5",
            Capacity = 32,
            ModuleCount = 1,
            Speed = 6000,
            Timing = "30-38-38-96",
            Voltage = 1.35,
            BasePrice = 150,
            Rating = 4.9,
            InStock = true,
            Description = "Высокоскоростная планка для игр и работы.",
            Tags = new List<string> { "performance", "DDR5" }
        },
        new Ram
        {
            Name = "Corsair Dominator Platinum 64GB DDR5-5600",
            Manufacturer = "Corsair",
            MemoryType = "DDR5",
            Capacity = 64,
            ModuleCount = 1,
            Speed = 5600,
            Timing = "40-40-40-80",
            Voltage = 1.25,
            BasePrice = 280,
            Rating = 4.8,
            InStock = true,
            Description = "Премиальная память с массивным радиатором.",
            Tags = new List<string> { "premium", "DDR5" }
        },

        // DDR5 – комплекты
        new Ram
        {
            Name = "Kingston Fury Beast 32GB (2x16GB) DDR5-5200",
            Manufacturer = "Kingston",
            MemoryType = "DDR5",
            Capacity = 16,
            ModuleCount = 2,
            Speed = 5200,
            Timing = "38-38-38-70",
            Voltage = 1.25,
            BasePrice = 140,
            Rating = 4.6,
            InStock = true,
            Description = "Доступный комплект DDR5 для первых сборок.",
            Tags = new List<string> { "kit", "DDR5" }
        },
        new Ram
        {
            Name = "G.Skill Trident Z5 RGB 64GB (2x32GB) DDR5-6400",
            Manufacturer = "G.Skill",
            MemoryType = "DDR5",
            Capacity = 32,
            ModuleCount = 2,
            Speed = 6400,
            Timing = "32-39-39-102",
            Voltage = 1.4,
            BasePrice = 320,
            Rating = 5.0,
            InStock = false,
            Description = "Топовый комплект с RGB и экстремальными частотами.",
            Tags = new List<string> { "flagship", "RGB", "kit", "DDR5" }
        },

        // Ещё несколько для разнообразия
        new Ram
        {
            Name = "Patriot Viper Steel 8GB DDR4-3000",
            Manufacturer = "Patriot",
            MemoryType = "DDR4",
            Capacity = 8,
            ModuleCount = 1,
            Speed = 3000,
            Timing = "16-18-18-36",
            Voltage = 1.35,
            BasePrice = 40,
            Rating = 4.4,
            InStock = true,
            Description = "Бюджетная игровая память.",
            Tags = new List<string> { "budget", "gaming", "DDR4" }
        },
        new Ram
        {
            Name = "Hynix 4GB DDR3L-1600",
            Manufacturer = "Hynix",
            MemoryType = "DDR3L",
            Capacity = 4,
            ModuleCount = 1,
            Speed = 1600,
            Timing = "11-11-11-28",
            Voltage = 1.35,
            BasePrice = 15,
            Rating = 3.9,
            InStock = true,
            Description = "Низковольтная память для ноутбуков и старых ПК.",
            Tags = new List<string> { "laptop", "DDR3" }
        },
        new Ram
        {
            Name = "Corsair Vengeance RGB RT 16GB DDR4-3600",
            Manufacturer = "Corsair",
            MemoryType = "DDR4",
            Capacity = 16,
            ModuleCount = 1,
            Speed = 3600,
            Timing = "16-19-19-36",
            Voltage = 1.35,
            BasePrice = 85,
            Rating = 4.7,
            InStock = true,
            Description = "Красивая RGB-память с хорошими таймингами.",
            Tags = new List<string> { "RGB", "performance", "DDR4" }
        },
        new Ram
        {
            Name = "TeamGroup Elite 8GB DDR4-2133",
            Manufacturer = "TeamGroup",
            MemoryType = "DDR4",
            Capacity = 8,
            ModuleCount = 1,
            Speed = 2133,
            Timing = "15-15-15-35",
            Voltage = 1.2,
            BasePrice = 28,
            Rating = 4.1,
            InStock = true,
            Description = "Простая и надёжная планка.",
            Tags = new List<string> { "budget", "DDR4" }
        },
        new Ram
        {
            Name = "Samsung 32GB DDR5-4800 ECC",
            Manufacturer = "Samsung",
            MemoryType = "DDR5",
            Capacity = 32,
            ModuleCount = 1,
            Speed = 4800,
            Timing = "40-39-39",
            Voltage = 1.1,
            BasePrice = 160,
            Rating = 4.5,
            InStock = false,
            Description = "Серверная память с ECC (корректировка ошибок).",
            Tags = new List<string> { "server", "ECC", "DDR5" }
        },
        new Ram
        {
            Name = "Kingston HyperX Predator 16GB DDR3-2133",
            Manufacturer = "Kingston",
            MemoryType = "DDR3",
            Capacity = 16,
            ModuleCount = 1,
            Speed = 2133,
            Timing = "11-13-13-30",
            Voltage = 1.65,
            BasePrice = 90,
            Rating = 4.6,
            InStock = false,
            Description = "Высокочастотная DDR3 для оверклокинга.",
            Tags = new List<string> { "overclocking", "DDR3" }
        }
    };
        }
        public void GetMotherboards()
        {
            Motherboards = new List<Motherboard>
{
    // Intel LGA1700 (12-14 поколение)
    new Motherboard
    {
        Name = "MSI PRO Z790-P",
        Manufacturer = "MSI",
        Chipset = "Intel Z790",
        MemoryType = "DDR5",
        MemorySlots = 4,
        MaxMemory = 128,
        MaxMemorySpeed = 7200,
        PcieVersion = "5.0",
        M2Slots = 4,
        SataPorts = 6,
        IntegratedWifi = false,
        IntegratedBluetooth = false,
        BasePrice = 200,
        Rating = 4.6,
        InStock = true,
        Description = "Надёжная материнская плата для процессоров Intel 12-14 поколения.",
        Tags = new List<string> { "ATX", "DDR5", "LGA1700" }
    },
    new Motherboard
    {
        Name = "Gigabyte B760 AORUS Elite",
        Manufacturer = "Gigabyte",
        Chipset = "Intel B760",
        MemoryType = "DDR5",
        MemorySlots = 4,
        MaxMemory = 128,
        MaxMemorySpeed = 7600,
        PcieVersion = "4.0",
        M2Slots = 3,
        SataPorts = 4,
        IntegratedWifi = true,
        IntegratedBluetooth = true,
        BasePrice = 180,
        Rating = 4.7,
        InStock = true,
        Description = "Хороший вариант для сборки среднего уровня с DDR5.",
        Tags = new List<string> { "ATX", "DDR5", "WiFi", "LGA1700" }
    },
    new Motherboard
    {
        Name = "ASUS Prime H610M-A",
        Manufacturer = "ASUS",
        Chipset = "Intel H610",
        MemoryType = "DDR4",
        MemorySlots = 2,
        MaxMemory = 64,
        MaxMemorySpeed = 3200,
        PcieVersion = "3.0",
        M2Slots = 1,
        SataPorts = 4,
        IntegratedWifi = false,
        IntegratedBluetooth = false,
        BasePrice = 90,
        Rating = 4.3,
        InStock = true,
        Description = "Бюджетная плата для офисных сборок на DDR4.",
        Tags = new List<string> { "Micro-ATX", "DDR4", "LGA1700", "budget" }
    },
    new Motherboard
    {
        Name = "ASRock Z790 Steel Legend",
        Manufacturer = "ASRock",
        Chipset = "Intel Z790",
        MemoryType = "DDR5",
        MemorySlots = 4,
        MaxMemory = 128,
        MaxMemorySpeed = 7200,
        PcieVersion = "5.0",
        M2Slots = 5,
        SataPorts = 8,
        IntegratedWifi = true,
        IntegratedBluetooth = true,
        BasePrice = 250,
        Rating = 4.8,
        InStock = false,
        Description = "Функциональная плата с хорошим набором портов.",
        Tags = new List<string> { "ATX", "DDR5", "WiFi", "LGA1700" }
    },

    // Intel LGA1200 (10-11 поколение)
    new Motherboard
    {
        Name = "MSI Z590-A PRO",
        Manufacturer = "MSI",
        Chipset = "Intel Z590",
        MemoryType = "DDR4",
        MemorySlots = 4,
        MaxMemory = 128,
        MaxMemorySpeed = 5333,
        PcieVersion = "4.0",
        M2Slots = 3,
        SataPorts = 6,
        IntegratedWifi = false,
        IntegratedBluetooth = false,
        BasePrice = 170,
        Rating = 4.5,
        InStock = true,
        Description = "Для процессоров 10-11 поколения Intel.",
        Tags = new List<string> { "ATX", "DDR4", "LGA1200" }
    },

    // AMD AM5 (Ryzen 7000)
    new Motherboard
    {
        Name = "ASUS ROG Strix B650E-F",
        Manufacturer = "ASUS",
        Chipset = "AMD B650E",
        MemoryType = "DDR5",
        MemorySlots = 4,
        MaxMemory = 128,
        MaxMemorySpeed = 6400,
        PcieVersion = "5.0",
        M2Slots = 3,
        SataPorts = 4,
        IntegratedWifi = true,
        IntegratedBluetooth = true,
        BasePrice = 280,
        Rating = 4.8,
        InStock = true,
        Description = "Отличная плата для Ryzen 7000 с PCIe 5.0.",
        Tags = new List<string> { "ATX", "DDR5", "WiFi", "AM5" }
    },
    new Motherboard
    {
        Name = "Gigabyte X670 AORUS Elite",
        Manufacturer = "Gigabyte",
        Chipset = "AMD X670",
        MemoryType = "DDR5",
        MemorySlots = 4,
        MaxMemory = 128,
        MaxMemorySpeed = 6600,
        PcieVersion = "5.0",
        M2Slots = 4,
        SataPorts = 6,
        IntegratedWifi = true,
        IntegratedBluetooth = true,
        BasePrice = 300,
        Rating = 4.9,
        InStock = true,
        Description = "Флагманский чипсет для максимальной производительности.",
        Tags = new List<string> { "ATX", "DDR5", "WiFi", "AM5" }
    },
    new Motherboard
    {
        Name = "ASRock A620M-HDV",
        Manufacturer = "ASRock",
        Chipset = "AMD A620",
        MemoryType = "DDR5",
        MemorySlots = 2,
        MaxMemory = 64,
        MaxMemorySpeed = 4800,
        PcieVersion = "4.0",
        M2Slots = 1,
        SataPorts = 4,
        IntegratedWifi = false,
        IntegratedBluetooth = false,
        BasePrice = 85,
        Rating = 4.1,
        InStock = true,
        Description = "Бюджетный вариант для Ryzen 7000 на DDR5.",
        Tags = new List<string> { "Micro-ATX", "DDR5", "AM5", "budget" }
    },
    new Motherboard
    {
        Name = "MSI MPG B650I Edge WiFi",
        Manufacturer = "MSI",
        Chipset = "AMD B650",
        MemoryType = "DDR5",
        MemorySlots = 2,
        MaxMemory = 64,
        MaxMemorySpeed = 6000,
        PcieVersion = "4.0",
        M2Slots = 2,
        SataPorts = 4,
        IntegratedWifi = true,
        IntegratedBluetooth = true,
        BasePrice = 220,
        Rating = 4.7,
        InStock = true,
        Description = "Компактная плата формата Mini-ITX.",
        Tags = new List<string> { "Mini-ITX", "DDR5", "WiFi", "AM5" }
    },

    // AMD AM4 (Ryzen 1000-5000)
    new Motherboard
    {
        Name = "MSI B550 Tomahawk",
        Manufacturer = "MSI",
        Chipset = "AMD B550",
        MemoryType = "DDR4",
        MemorySlots = 4,
        MaxMemory = 128,
        MaxMemorySpeed = 5100,
        PcieVersion = "4.0",
        M2Slots = 2,
        SataPorts = 6,
        IntegratedWifi = false,
        IntegratedBluetooth = false,
        BasePrice = 150,
        Rating = 4.8,
        InStock = true,
        Description = "Одна из лучших плат для Ryzen 3000/5000.",
        Tags = new List<string> { "ATX", "DDR4", "AM4" }
    },
    new Motherboard
    {
        Name = "ASUS ROG Crosshair VIII Hero",
        Manufacturer = "ASUS",
        Chipset = "AMD X570",
        MemoryType = "DDR4",
        MemorySlots = 4,
        MaxMemory = 128,
        MaxMemorySpeed = 5100,
        PcieVersion = "4.0",
        M2Slots = 3,
        SataPorts = 8,
        IntegratedWifi = true,
        IntegratedBluetooth = true,
        BasePrice = 400,
        Rating = 4.9,
        InStock = false,
        Description = "Премиальная плата для энтузиастов.",
        Tags = new List<string> { "ATX", "DDR4", "WiFi", "AM4", "flagship" }
    },
    new Motherboard
    {
        Name = "Gigabyte A520M DS3H",
        Manufacturer = "Gigabyte",
        Chipset = "AMD A520",
        MemoryType = "DDR4",
        MemorySlots = 4,
        MaxMemory = 64,
        MaxMemorySpeed = 4733,
        PcieVersion = "3.0",
        M2Slots = 1,
        SataPorts = 4,
        IntegratedWifi = false,
        IntegratedBluetooth = false,
        BasePrice = 70,
        Rating = 4.3,
        InStock = true,
        Description = "Бюджетная Micro-ATX для офисных сборок.",
        Tags = new List<string> { "Micro-ATX", "DDR4", "AM4", "budget" }
    },
    new Motherboard
    {
        Name = "ASRock B450M Pro4",
        Manufacturer = "ASRock",
        Chipset = "AMD B450",
        MemoryType = "DDR4",
        MemorySlots = 4,
        MaxMemory = 64,
        MaxMemorySpeed = 3200,
        PcieVersion = "3.0",
        M2Slots = 1,
        SataPorts = 6,
        IntegratedWifi = false,
        IntegratedBluetooth = false,
        BasePrice = 80,
        Rating = 4.5,
        InStock = true,
        Description = "Проверенная временем модель для Ryzen 1000-3000.",
        Tags = new List<string> { "Micro-ATX", "DDR4", "AM4" }
    },

    // HEDT (Threadripper)
    new Motherboard
    {
        Name = "ASUS TRX40 Prime",
        Manufacturer = "ASUS",
        Chipset = "AMD TRX40",
        MemoryType = "DDR4",
        MemorySlots = 8,
        MaxMemory = 256,
        MaxMemorySpeed = 4600,
        PcieVersion = "4.0",
        M2Slots = 3,
        SataPorts = 8,
        IntegratedWifi = true,
        IntegratedBluetooth = true,
        BasePrice = 600,
        Rating = 4.8,
        InStock = false,
        Description = "Плата для процессоров Threadripper 3000.",
        Tags = new List<string> { "E-ATX", "DDR4", "WiFi", "TRX40", "HEDT" }
    },

    // Дополнительные для заполнения до 20
    new Motherboard
    {
        Name = "MSI Z690-A Pro",
        Manufacturer = "MSI",
        Chipset = "Intel Z690",
        MemoryType = "DDR4",
        MemorySlots = 4,
        MaxMemory = 128,
        MaxMemorySpeed = 5333,
        PcieVersion = "5.0",
        M2Slots = 4,
        SataPorts = 6,
        IntegratedWifi = false,
        IntegratedBluetooth = false,
        BasePrice = 180,
        Rating = 4.6,
        InStock = true,
        Description = "Надёжная плата для Intel 12 поколения с DDR4.",
        Tags = new List<string> { "ATX", "DDR4", "LGA1700" }
    },
    new Motherboard
    {
        Name = "Gigabyte B660M DS3H",
        Manufacturer = "Gigabyte",
        Chipset = "Intel B660",
        MemoryType = "DDR4",
        MemorySlots = 4,
        MaxMemory = 64,
        MaxMemorySpeed = 3200,
        PcieVersion = "4.0",
        M2Slots = 2,
        SataPorts = 4,
        IntegratedWifi = false,
        IntegratedBluetooth = false,
        BasePrice = 100,
        Rating = 4.4,
        InStock = true,
        Description = "Популярная Micro-ATX для Intel 12-13 поколения.",
        Tags = new List<string> { "Micro-ATX", "DDR4", "LGA1700", "budget" }
    },
    new Motherboard
    {
        Name = "ASUS ROG Strix X670E-E",
        Manufacturer = "ASUS",
        Chipset = "AMD X670E",
        MemoryType = "DDR5",
        MemorySlots = 4,
        MaxMemory = 128,
        MaxMemorySpeed = 6400,
        PcieVersion = "5.0",
        M2Slots = 5,
        SataPorts = 6,
        IntegratedWifi = true,
        IntegratedBluetooth = true,
        BasePrice = 500,
        Rating = 4.9,
        InStock = true,
        Description = "Топовая плата для Ryzen с PCIe 5.0 на всех слотах.",
        Tags = new List<string> { "ATX", "DDR5", "WiFi", "AM5", "flagship" }
    },
    new Motherboard
    {
        Name = "ASRock B550M Steel Legend",
        Manufacturer = "ASRock",
        Chipset = "AMD B550",
        MemoryType = "DDR4",
        MemorySlots = 4,
        MaxMemory = 128,
        MaxMemorySpeed = 4733,
        PcieVersion = "4.0",
        M2Slots = 2,
        SataPorts = 6,
        IntegratedWifi = false,
        IntegratedBluetooth = false,
        BasePrice = 130,
        Rating = 4.7,
        InStock = true,
        Description = "Стильная Micro-ATX с хорошей начинкой.",
        Tags = new List<string> { "Micro-ATX", "DDR4", "AM4" }
    },
    new Motherboard
    {
        Name = "MSI MPG Z790 Carbon WiFi",
        Manufacturer = "MSI",
        Chipset = "Intel Z790",
        MemoryType = "DDR5",
        MemorySlots = 4,
        MaxMemory = 128,
        MaxMemorySpeed = 7600,
        PcieVersion = "5.0",
        M2Slots = 5,
        SataPorts = 7,
        IntegratedWifi = true,
        IntegratedBluetooth = true,
        BasePrice = 350,
        Rating = 4.8,
        InStock = true,
        Description = "Продвинутая плата с поддержкой быстрой DDR5.",
        Tags = new List<string> { "ATX", "DDR5", "WiFi", "LGA1700" }
    },
    new Motherboard
    {
        Name = "Gigabyte H610M S2H",
        Manufacturer = "Gigabyte",
        Chipset = "Intel H610",
        MemoryType = "DDR4",
        MemorySlots = 2,
        MaxMemory = 64,
        MaxMemorySpeed = 3200,
        PcieVersion = "3.0",
        M2Slots = 1,
        SataPorts = 4,
        IntegratedWifi = false,
        IntegratedBluetooth = false,
        BasePrice = 75,
        Rating = 4.2,
        InStock = true,
        Description = "Простая и недорогая плата для офиса.",
        Tags = new List<string> { "Micro-ATX", "DDR4", "LGA1700", "budget" }
    },
    new Motherboard
    {
        Name = "ASUS Prime B450M-A II",
        Manufacturer = "ASUS",
        Chipset = "AMD B450",
        MemoryType = "DDR4",
        MemorySlots = 4,
        MaxMemory = 64,
        MaxMemorySpeed = 3200,
        PcieVersion = "3.0",
        M2Slots = 1,
        SataPorts = 6,
        IntegratedWifi = false,
        IntegratedBluetooth = false,
        BasePrice = 80,
        Rating = 4.4,
        InStock = true,
        Description = "Обновлённая версия популярной B450M.",
        Tags = new List<string> { "Micro-ATX", "DDR4", "AM4" }
    },
    new Motherboard
    {
        Name = "MSI B650 Tomahawk WiFi",
        Manufacturer = "MSI",
        Chipset = "AMD B650",
        MemoryType = "DDR5",
        MemorySlots = 4,
        MaxMemory = 128,
        MaxMemorySpeed = 6400,
        PcieVersion = "4.0",
        M2Slots = 3,
        SataPorts = 6,
        IntegratedWifi = true,
        IntegratedBluetooth = true,
        BasePrice = 210,
        Rating = 4.7,
        InStock = true,
        Description = "Хороший баланс цены и возможностей для AM5.",
        Tags = new List<string> { "ATX", "DDR5", "WiFi", "AM5" }
    }
};
        }
        public void GetGpu()
        {
            Gpus = new List<Gpu>
    {
        // NVIDIA GeForce RTX 30/40 серии
        new Gpu
        {
            Name = "GeForce RTX 3060",
            Manufacturer = "NVIDIA",
            MemorySize = 12,
            MemoryType = "GDDR6",
            CoreClock = 1320,
            BoostClock = 1777,
            Tdp = 170,
            Length = 242,
            PowerConnectors = "1x8-pin",
            HdmiPorts = 1,
            DisplayPorts = 3,
            BasePrice = 330,
            Rating = 4.7,
            InStock = true,
            Description = "Популярная видеокарта для игр в 1080p и начального 1440p.",
            Tags = new List<string> { "gaming", "mid-range", "NVIDIA" }
        },
        new Gpu
        {
            Name = "GeForce RTX 3070",
            Manufacturer = "NVIDIA",
            MemorySize = 8,
            MemoryType = "GDDR6",
            CoreClock = 1500,
            BoostClock = 1725,
            Tdp = 220,
            Length = 267,
            PowerConnectors = "1x12-pin",
            HdmiPorts = 1,
            DisplayPorts = 3,
            BasePrice = 500,
            Rating = 4.8,
            InStock = true,
            Description = "Отличная карта для 1440p игр с высокими настройками.",
            Tags = new List<string> { "gaming", "high-end", "NVIDIA" }
        },
        new Gpu
        {
            Name = "GeForce RTX 3080",
            Manufacturer = "NVIDIA",
            MemorySize = 10,
            MemoryType = "GDDR6X",
            CoreClock = 1440,
            BoostClock = 1710,
            Tdp = 320,
            Length = 285,
            PowerConnectors = "2x8-pin",
            HdmiPorts = 1,
            DisplayPorts = 3,
            BasePrice = 700,
            Rating = 4.9,
            InStock = false,
            Description = "Мощная карта для 4K игр и профессиональных задач.",
            Tags = new List<string> { "gaming", "enthusiast", "NVIDIA" }
        },
        new Gpu
        {
            Name = "GeForce RTX 4090",
            Manufacturer = "NVIDIA",
            MemorySize = 24,
            MemoryType = "GDDR6X",
            CoreClock = 2235,
            BoostClock = 2520,
            Tdp = 450,
            Length = 336,
            PowerConnectors = "1x12VHPWR",
            HdmiPorts = 1,
            DisplayPorts = 3,
            BasePrice = 1600,
            Rating = 5.0,
            InStock = true,
            Description = "Флагманская видеокарта с высочайшей производительностью.",
            Tags = new List<string> { "flagship", "4K", "NVIDIA" }
        },
        new Gpu
        {
            Name = "GeForce RTX 4060 Ti",
            Manufacturer = "NVIDIA",
            MemorySize = 8,
            MemoryType = "GDDR6",
            CoreClock = 2310,
            BoostClock = 2535,
            Tdp = 160,
            Length = 240,
            PowerConnectors = "1x8-pin",
            HdmiPorts = 1,
            DisplayPorts = 3,
            BasePrice = 400,
            Rating = 4.6,
            InStock = true,
            Description = "Новинка среднего сегмента для 1080p и 1440p.",
            Tags = new List<string> { "gaming", "mid-range", "NVIDIA" }
        },

        // AMD Radeon RX 6000/7000 серии
        new Gpu
        {
            Name = "Radeon RX 6600",
            Manufacturer = "AMD",
            MemorySize = 8,
            MemoryType = "GDDR6",
            CoreClock = 1626,
            BoostClock = 2491,
            Tdp = 132,
            Length = 190,
            PowerConnectors = "1x8-pin",
            HdmiPorts = 1,
            DisplayPorts = 3,
            BasePrice = 280,
            Rating = 4.6,
            InStock = true,
            Description = "Бюджетная карта для игр в 1080p.",
            Tags = new List<string> { "budget", "gaming", "AMD" }
        },
        new Gpu
        {
            Name = "Radeon RX 6700 XT",
            Manufacturer = "AMD",
            MemorySize = 12,
            MemoryType = "GDDR6",
            CoreClock = 2321,
            BoostClock = 2581,
            Tdp = 230,
            Length = 267,
            PowerConnectors = "2x8-pin",
            HdmiPorts = 1,
            DisplayPorts = 3,
            BasePrice = 450,
            Rating = 4.8,
            InStock = true,
            Description = "Отличный выбор для 1440p игр.",
            Tags = new List<string> { "gaming", "mid-range", "AMD" }
        },
        new Gpu
        {
            Name = "Radeon RX 6800 XT",
            Manufacturer = "AMD",
            MemorySize = 16,
            MemoryType = "GDDR6",
            CoreClock = 1825,
            BoostClock = 2250,
            Tdp = 300,
            Length = 267,
            PowerConnectors = "2x8-pin",
            HdmiPorts = 1,
            DisplayPorts = 2,
            BasePrice = 650,
            Rating = 4.9,
            InStock = false,
            Description = "Мощная карта для 4K игр.",
            Tags = new List<string> { "high-end", "4K", "AMD" }
        },
        new Gpu
        {
            Name = "Radeon RX 7900 XTX",
            Manufacturer = "AMD",
            MemorySize = 24,
            MemoryType = "GDDR6",
            CoreClock = 1855,
            BoostClock = 2500,
            Tdp = 355,
            Length = 287,
            PowerConnectors = "2x8-pin",
            HdmiPorts = 1,
            DisplayPorts = 2,
            BasePrice = 1000,
            Rating = 5.0,
            InStock = true,
            Description = "Флагман AMD с огромной производительностью.",
            Tags = new List<string> { "flagship", "4K", "AMD" }
        },
        new Gpu
        {
            Name = "Radeon RX 7600",
            Manufacturer = "AMD",
            MemorySize = 8,
            MemoryType = "GDDR6",
            CoreClock = 1720,
            BoostClock = 2650,
            Tdp = 165,
            Length = 204,
            PowerConnectors = "1x8-pin",
            HdmiPorts = 1,
            DisplayPorts = 3,
            BasePrice = 270,
            Rating = 4.5,
            InStock = true,
            Description = "Новинка для игр 1080p.",
            Tags = new List<string> { "budget", "gaming", "AMD" }
        },

        // Intel Arc
        new Gpu
        {
            Name = "Arc A750",
            Manufacturer = "Intel",
            MemorySize = 8,
            MemoryType = "GDDR6",
            CoreClock = 2050,
            BoostClock = 2400,
            Tdp = 225,
            Length = 280,
            PowerConnectors = "2x8-pin",
            HdmiPorts = 1,
            DisplayPorts = 3,
            BasePrice = 250,
            Rating = 4.3,
            InStock = true,
            Description = "Видеокарта Intel среднего уровня, хороша для новых игр.",
            Tags = new List<string> { "mid-range", "gaming", "Intel" }
        },
        new Gpu
        {
            Name = "Arc A770",
            Manufacturer = "Intel",
            MemorySize = 16,
            MemoryType = "GDDR6",
            CoreClock = 2100,
            BoostClock = 2400,
            Tdp = 225,
            Length = 280,
            PowerConnectors = "2x8-pin",
            HdmiPorts = 1,
            DisplayPorts = 3,
            BasePrice = 350,
            Rating = 4.5,
            InStock = false,
            Description = "Флагман Intel с большим объёмом памяти.",
            Tags = new List<string> { "high-end", "gaming", "Intel" }
        },

        // Дополнительные модели для заполнения до 20
        new Gpu
        {
            Name = "GeForce GTX 1660 Super",
            Manufacturer = "NVIDIA",
            MemorySize = 6,
            MemoryType = "GDDR6",
            CoreClock = 1530,
            BoostClock = 1785,
            Tdp = 125,
            Length = 229,
            PowerConnectors = "1x8-pin",
            HdmiPorts = 1,
            DisplayPorts = 1,
            BasePrice = 230,
            Rating = 4.6,
            InStock = true,
            Description = "Проверенная классика для 1080p.",
            Tags = new List<string> { "budget", "gaming", "NVIDIA" }
        },
        new Gpu
        {
            Name = "GeForce RTX 3050",
            Manufacturer = "NVIDIA",
            MemorySize = 8,
            MemoryType = "GDDR6",
            CoreClock = 1552,
            BoostClock = 1777,
            Tdp = 130,
            Length = 242,
            PowerConnectors = "1x8-pin",
            HdmiPorts = 1,
            DisplayPorts = 3,
            BasePrice = 250,
            Rating = 4.4,
            InStock = true,
            Description = "Начальная карта с поддержкой RTX.",
            Tags = new List<string> { "budget", "raytracing", "NVIDIA" }
        },
        new Gpu
        {
            Name = "Radeon RX 6500 XT",
            Manufacturer = "AMD",
            MemorySize = 4,
            MemoryType = "GDDR6",
            CoreClock = 2310,
            BoostClock = 2815,
            Tdp = 107,
            Length = 201,
            PowerConnectors = "нет",
            HdmiPorts = 1,
            DisplayPorts = 1,
            BasePrice = 150,
            Rating = 3.9,
            InStock = true,
            Description = "Бюджетная карта для нетребовательных игр.",
            Tags = new List<string> { "budget", "office", "AMD" }
        },
        new Gpu
        {
            Name = "GeForce RTX 4070 Ti",
            Manufacturer = "NVIDIA",
            MemorySize = 12,
            MemoryType = "GDDR6X",
            CoreClock = 2310,
            BoostClock = 2610,
            Tdp = 285,
            Length = 336,
            PowerConnectors = "1x12VHPWR",
            HdmiPorts = 1,
            DisplayPorts = 3,
            BasePrice = 800,
            Rating = 4.9,
            InStock = true,
            Description = "Мощная карта для 4K игр с DLSS 3.",
            Tags = new List<string> { "high-end", "4K", "NVIDIA" }
        },
        new Gpu
        {
            Name = "Radeon RX 7700 XT",
            Manufacturer = "AMD",
            MemorySize = 12,
            MemoryType = "GDDR6",
            CoreClock = 1900,
            BoostClock = 2800,
            Tdp = 245,
            Length = 267,
            PowerConnectors = "2x8-pin",
            HdmiPorts = 1,
            DisplayPorts = 3,
            BasePrice = 450,
            Rating = 4.7,
            InStock = true,
            Description = "Свежая модель для 1440p.",
            Tags = new List<string> { "mid-range", "gaming", "AMD" }
        },
        new Gpu
        {
            Name = "Arc A380",
            Manufacturer = "Intel",
            MemorySize = 6,
            MemoryType = "GDDR6",
            CoreClock = 2000,
            BoostClock = 2450,
            Tdp = 75,
            Length = 183,
            PowerConnectors = "нет",
            HdmiPorts = 1,
            DisplayPorts = 3,
            BasePrice = 140,
            Rating = 3.8,
            InStock = true,
            Description = "Начальная карта для офиса и простых игр.",
            Tags = new List<string> { "budget", "office", "Intel" }
        },
        new Gpu
        {
            Name = "GeForce RTX 4080",
            Manufacturer = "NVIDIA",
            MemorySize = 16,
            MemoryType = "GDDR6X",
            CoreClock = 2205,
            BoostClock = 2505,
            Tdp = 320,
            Length = 336,
            PowerConnectors = "1x12VHPWR",
            HdmiPorts = 1,
            DisplayPorts = 3,
            BasePrice = 1200,
            Rating = 5.0,
            InStock = false,
            Description = "Премиальная карта для максимальных настроек.",
            Tags = new List<string> { "flagship", "4K", "NVIDIA" }
        },
        new Gpu
        {
            Name = "Radeon RX 6600 XT",
            Manufacturer = "AMD",
            MemorySize = 8,
            MemoryType = "GDDR6",
            CoreClock = 1968,
            BoostClock = 2589,
            Tdp = 160,
            Length = 190,
            PowerConnectors = "1x8-pin",
            HdmiPorts = 1,
            DisplayPorts = 3,
            BasePrice = 350,
            Rating = 4.7,
            InStock = true,
            Description = "Ускоренная версия для 1080p-1440p.",
            Tags = new List<string> { "mid-range", "gaming", "AMD" }
        },
        new Gpu
        {
            Name = "GeForce GTX 1050 Ti",
            Manufacturer = "NVIDIA",
            MemorySize = 4,
            MemoryType = "GDDR5",
            CoreClock = 1290,
            BoostClock = 1392,
            Tdp = 75,
            Length = 145,
            PowerConnectors = "нет",
            HdmiPorts = 1,
            DisplayPorts = 1,
            BasePrice = 120,
            Rating = 4.0,
            InStock = true,
            Description = "Легендарная карта для старых систем.",
            Tags = new List<string> { "budget", "office", "NVIDIA" }
        },
        new Gpu
        {
            Name = "Radeon RX 6400",
            Manufacturer = "AMD",
            MemorySize = 4,
            MemoryType = "GDDR6",
            CoreClock = 2039,
            BoostClock = 2321,
            Tdp = 53,
            Length = 170,
            PowerConnectors = "нет",
            HdmiPorts = 1,
            DisplayPorts = 1,
            BasePrice = 130,
            Rating = 3.7,
            InStock = true,
            Description = "Простая карта без дополнительного питания.",
            Tags = new List<string> { "budget", "office", "AMD" }
        }
    };
        }
        public void GetCpu()
        {
            Cpus = new List<Cpu>
        {
            // Intel
            new Cpu
            {
                Name = "Core i3-12100",
                Manufacturer = "Intel",
                Series = "Core i3",
                CoreCount = 4,
                ThreadCount = 8,
                BaseClock = 3.3,
                BoostClock = 4.3,
                IntegratedGraphics = true,
                MaxMemorySpeed = 4800,
                BasePrice = 120,
                Rating = 4.5,
                InStock = true,
                Description = "Бюджетный процессор для офисных задач и нетребовательных игр.",
                Tags = new List<string> { "budget", "office", "LGA1700" }
            },
            new Cpu
            {
                Name = "Core i5-12400",
                Manufacturer = "Intel",
                Series = "Core i5",
                CoreCount = 6,
                ThreadCount = 12,
                BaseClock = 2.5,
                BoostClock = 4.4,
                IntegratedGraphics = true,
                MaxMemorySpeed = 4800,
                BasePrice = 200,
                Rating = 4.7,
                InStock = true,
                Description = "Отличный баланс цены и производительности для игр и работы.",
                Tags = new List<string> { "mid-range", "gaming", "LGA1700" }
            },
            new Cpu
            {
                Name = "Core i5-12600K",
                Manufacturer = "Intel",
                Series = "Core i5",
                CoreCount = 10,
                ThreadCount = 16,
                BaseClock = 3.7,
                BoostClock = 4.9,
                IntegratedGraphics = true,
                MaxMemorySpeed = 4800,
                BasePrice = 280,
                Rating = 4.8,
                InStock = true,
                Description = "Мощный процессор с разблокированным множителем для энтузиастов.",
                Tags = new List<string> { "performance", "unlocked", "LGA1700" }
            },
            new Cpu
            {
                Name = "Core i7-12700K",
                Manufacturer = "Intel",
                Series = "Core i7",
                CoreCount = 12,
                ThreadCount = 20,
                BaseClock = 3.6,
                BoostClock = 5.0,
                IntegratedGraphics = true,
                MaxMemorySpeed = 4800,
                BasePrice = 400,
                Rating = 4.9,
                InStock = true,
                Description = "Высокопроизводительный процессор для игр и тяжёлых задач.",
                Tags = new List<string> { "high-end", "gaming", "LGA1700" }
            },
            new Cpu
            {
                Name = "Core i9-12900K",
                Manufacturer = "Intel",
                Series = "Core i9",
                CoreCount = 16,
                ThreadCount = 24,
                BaseClock = 3.2,
                BoostClock = 5.2,
                IntegratedGraphics = true,
                MaxMemorySpeed = 4800,
                BasePrice = 600,
                Rating = 4.9,
                InStock = false,
                Description = "Флагманский процессор для максимальной производительности.",
                Tags = new List<string> { "flagship", "workstation", "LGA1700" }
            },
            new Cpu
            {
                Name = "Core i9-13900K",
                Manufacturer = "Intel",
                Series = "Core i9",
                CoreCount = 24,
                ThreadCount = 32,
                BaseClock = 3.0,
                BoostClock = 5.8,
                IntegratedGraphics = true,
                MaxMemorySpeed = 5600,
                BasePrice = 700,
                Rating = 5.0,
                InStock = true,
                Description = "Новейший флагман Intel с гибридной архитектурой.",
                Tags = new List<string> { "flagship", "gaming", "LGA1700", "Raptor Lake" }
            },
            new Cpu
            {
                Name = "Core i7-13700K",
                Manufacturer = "Intel",
                Series = "Core i7",
                CoreCount = 16,
                ThreadCount = 24,
                BaseClock = 3.4,
                BoostClock = 5.4,
                IntegratedGraphics = true,
                MaxMemorySpeed = 5600,
                BasePrice = 450,
                Rating = 4.9,
                InStock = true,
                Description = "Процессор для требовательных игр и творческих задач.",
                Tags = new List<string> { "high-end", "gaming", "LGA1700", "Raptor Lake" }
            },
            new Cpu
            {
                Name = "Core i5-13400",
                Manufacturer = "Intel",
                Series = "Core i5",
                CoreCount = 10,
                ThreadCount = 16,
                BaseClock = 2.5,
                BoostClock = 4.6,
                IntegratedGraphics = true,
                MaxMemorySpeed = 4800,
                BasePrice = 230,
                Rating = 4.7,
                InStock = true,
                Description = "Сбалансированный процессор нового поколения.",
                Tags = new List<string> { "mid-range", "LGA1700", "Raptor Lake" }
            },
            new Cpu
            {
                Name = "Core i3-13100",
                Manufacturer = "Intel",
                Series = "Core i3",
                CoreCount = 4,
                ThreadCount = 8,
                BaseClock = 3.4,
                BoostClock = 4.5,
                IntegratedGraphics = true,
                MaxMemorySpeed = 4800,
                BasePrice = 130,
                Rating = 4.4,
                InStock = true,
                Description = "Бюджетный вариант для офиса и дома.",
                Tags = new List<string> { "budget", "LGA1700" }
            },
            new Cpu
            {
                Name = "Core i9-11900K",
                Manufacturer = "Intel",
                Series = "Core i9",
                CoreCount = 8,
                ThreadCount = 16,
                BaseClock = 3.5,
                BoostClock = 5.3,
                IntegratedGraphics = true,
                MaxMemorySpeed = 3200,
                BasePrice = 500,
                Rating = 4.6,
                InStock = false,
                Description = "Предыдущее поколение флагмана, всё ещё актуальное.",
                Tags = new List<string> { "high-end", "LGA1200" }
            },

            // AMD
            new Cpu
            {
                Name = "Ryzen 3 3100",
                Manufacturer = "AMD",
                Series = "Ryzen 3",
                CoreCount = 4,
                ThreadCount = 8,
                BaseClock = 3.6,
                BoostClock = 3.9,
                IntegratedGraphics = false,
                MaxMemorySpeed = 3200,
                BasePrice = 100,
                Rating = 4.3,
                InStock = true,
                Description = "Доступный процессор для сборки начального уровня.",
                Tags = new List<string> { "budget", "AM4" }
            },
            new Cpu
            {
                Name = "Ryzen 5 5600X",
                Manufacturer = "AMD",
                Series = "Ryzen 5",
                CoreCount = 6,
                ThreadCount = 12,
                BaseClock = 3.7,
                BoostClock = 4.6,
                IntegratedGraphics = false,
                MaxMemorySpeed = 3200,
                BasePrice = 200,
                Rating = 4.8,
                InStock = true,
                Description = "Один из лучших игровых процессоров своего времени.",
                Tags = new List<string> { "mid-range", "gaming", "AM4" }
            },
            new Cpu
            {
                Name = "Ryzen 7 5800X",
                Manufacturer = "AMD",
                Series = "Ryzen 7",
                CoreCount = 8,
                ThreadCount = 16,
                BaseClock = 3.8,
                BoostClock = 4.7,
                IntegratedGraphics = false,
                MaxMemorySpeed = 3200,
                BasePrice = 300,
                Rating = 4.8,
                InStock = true,
                Description = "Мощный 8-ядерный процессор для игр и работы.",
                Tags = new List<string> { "performance", "AM4" }
            },
            new Cpu
            {
                Name = "Ryzen 9 5900X",
                Manufacturer = "AMD",
                Series = "Ryzen 9",
                CoreCount = 12,
                ThreadCount = 24,
                BaseClock = 3.7,
                BoostClock = 4.8,
                IntegratedGraphics = false,
                MaxMemorySpeed = 3200,
                BasePrice = 450,
                Rating = 4.9,
                InStock = true,
                Description = "Высокопроизводительный процессор для энтузиастов.",
                Tags = new List<string> { "high-end", "workstation", "AM4" }
            },
            new Cpu
            {
                Name = "Ryzen 9 5950X",
                Manufacturer = "AMD",
                Series = "Ryzen 9",
                CoreCount = 16,
                ThreadCount = 32,
                BaseClock = 3.4,
                BoostClock = 4.9,
                IntegratedGraphics = false,
                MaxMemorySpeed = 3200,
                BasePrice = 700,
                Rating = 5.0,
                InStock = false,
                Description = "Флагманский процессор для самых тяжёлых задач.",
                Tags = new List<string> { "flagship", "workstation", "AM4" }
            },
            new Cpu
            {
                Name = "Ryzen 5 7600X",
                Manufacturer = "AMD",
                Series = "Ryzen 5",
                CoreCount = 6,
                ThreadCount = 12,
                BaseClock = 4.7,
                BoostClock = 5.3,
                IntegratedGraphics = true,
                MaxMemorySpeed = 5200,
                BasePrice = 250,
                Rating = 4.8,
                InStock = true,
                Description = "Новейший процессор на архитектуре Zen 4.",
                Tags = new List<string> { "mid-range", "gaming", "AM5", "DDR5" }
            },
            new Cpu
            {
                Name = "Ryzen 7 7700X",
                Manufacturer = "AMD",
                Series = "Ryzen 7",
                CoreCount = 8,
                ThreadCount = 16,
                BaseClock = 4.5,
                BoostClock = 5.4,
                IntegratedGraphics = true,
                MaxMemorySpeed = 5200,
                BasePrice = 350,
                Rating = 4.9,
                InStock = true,
                Description = "Мощный процессор для игр и создания контента.",
                Tags = new List<string> { "performance", "AM5", "DDR5" }
            },
            new Cpu
            {
                Name = "Ryzen 9 7900X",
                Manufacturer = "AMD",
                Series = "Ryzen 9",
                CoreCount = 12,
                ThreadCount = 24,
                BaseClock = 4.7,
                BoostClock = 5.6,
                IntegratedGraphics = true,
                MaxMemorySpeed = 5200,
                BasePrice = 550,
                Rating = 4.9,
                InStock = true,
                Description = "Высокопроизводительный процессор нового поколения.",
                Tags = new List<string> { "high-end", "workstation", "AM5" }
            },
            new Cpu
            {
                Name = "Ryzen 9 7950X",
                Manufacturer = "AMD",
                Series = "Ryzen 9",
                CoreCount = 16,
                ThreadCount = 32,
                BaseClock = 4.5,
                BoostClock = 5.7,
                IntegratedGraphics = true,
                MaxMemorySpeed = 5200,
                BasePrice = 800,
                Rating = 5.0,
                InStock = false,
                Description = "Флагман AMD с 16 ядрами и высокой частотой.",
                Tags = new List<string> { "flagship", "AM5", "DDR5" }
            },
            new Cpu
            {
                Name = "Threadripper 3970X",
                Manufacturer = "AMD",
                Series = "Threadripper",
                CoreCount = 32,
                ThreadCount = 64,
                BaseClock = 3.7,
                BoostClock = 4.5,
                IntegratedGraphics = false,
                MaxMemorySpeed = 3200,
                BasePrice = 2000,
                Rating = 4.9,
                InStock = false,
                Description = "Процессор для профессиональных рабочих станций.",
                Tags = new List<string> { "workstation", "HEDT", "sTRX4" }
            }
        };
        }

        public IEnumerable<T> GetComponents<T>() where T : IComponent
        {
            if (typeof(T) == typeof(Cpu)) return Cpus.Cast<T>();
            if (typeof(T) == typeof(Motherboard)) return Motherboards.Cast<T>();
            if (typeof(T) == typeof(Ram)) return Rams.Cast<T>();
            if (typeof(T) == typeof(Gpu)) return Gpus.Cast<T>();
            if (typeof(T) == typeof(Storage)) return Storages.Cast<T>();
            if (typeof(T) == typeof(Psu)) return Psus.Cast<T>();
            if (typeof(T) == typeof(Case)) return Cases.Cast<T>();
            if (typeof(T) == typeof(Cooler)) return Coolers.Cast<T>();
            return [];
        }


    }
}
