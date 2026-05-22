using ComputerConfiguration.DB;
using ComputerConfiguration.Models;
using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Components;
using ComputerConfiguration.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text.Json;

namespace ComputerConfiguration.Infrastructure.Seeding;

/// <summary>
/// Читает JSON-файлы, созданные dns_parser.py, и заполняет БД.
/// Запуск: добавь вызов DatabaseSeeder.SeedAsync(context) в Program.cs
/// под флагом --seed или проверкой !context.Cpus.Any()
/// </summary>
public static class DatabaseSeeder
{
    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static async Task SeedAsync(ComputerConfigurationDBContext context, string dataFolder = "parsed_data")
    {
        //await context.Database.EnsureCreatedAsync();

        if (!await context.Cpus.AnyAsync())
            await SeedCpus(context, dataFolder);

        if (!await context.Gpus.AnyAsync())
            await SeedGpus(context, dataFolder);

        if (!await context.Motherboards.AnyAsync())
            await SeedMotherboards(context, dataFolder);

        if (!await context.Rams.AnyAsync())
            await SeedRam(context, dataFolder);

        if (!await context.Storages.AnyAsync())
        {
            await SeedStorage(context, Path.Combine(dataFolder, "storage_ssd.json"));
            await SeedStorage(context, Path.Combine(dataFolder, "storage_hdd.json"));
        }

        if (!await context.Psus.AnyAsync())
            await SeedPsu(context, dataFolder);

        if (!await context.Cases.AnyAsync())
            await SeedCases(context, dataFolder);

        if (!await context.Coolers.AnyAsync())
            await SeedCoolers(context, dataFolder);

        if (!await context.AdditionalServices.AnyAsync())
            await SeedAddServices(context);
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private static List<JsonElement> ReadJson(string path)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine($"[Seeder] Файл не найден: {path}");
            return [];
        }
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<JsonElement>>(json, JsonOpts) ?? [];
    }

    private static byte[]? LoadImage(JsonElement el)
    {
        if (!el.TryGetProperty("ImagePath", out var pathProp)) return null;
        var path = Path.Combine("C:\\Users\\user\\Desktop\\ООП", pathProp.GetString());
        if (string.IsNullOrEmpty(path) || !File.Exists(path)) return null;
        return File.ReadAllBytes(path);
    }

    private static string S(JsonElement el, string key) =>
        el.TryGetProperty(key, out var v) ? v.GetString() ?? "" : "";

    private static int I(JsonElement el, string key) =>
        el.TryGetProperty(key, out var v) && v.TryGetInt32(out var i) ? i : 0;

    private static double D(JsonElement el, string key) =>
        el.TryGetProperty(key, out var v) && v.TryGetDouble(out var d) ? d : 0;

    private static bool B(JsonElement el, string key) =>
        el.TryGetProperty(key, out var v) && v.ValueKind == JsonValueKind.True;

    private static List<string> Tags(JsonElement el)
    {
        if (!el.TryGetProperty("Tags", out var arr)) return [];
        return arr.EnumerateArray().Select(t => t.GetString() ?? "").Where(t => t != "").ToList();
    }

    private static void FillBase(ComponentBase comp, JsonElement el)
    {
        comp.Name = S(el, "Name");
        comp.Manufacturer = S(el, "Manufacturer");
        comp.BasePrice = D(el, "BasePrice");
        comp.Rating = D(el, "Rating");
        comp.InStock = B(el, "InStock");
        comp.Description = S(el, "Description");
        comp.ImageData = LoadImage(el);
        comp.Tags = Tags(el);
    }

    // ── CPU ───────────────────────────────────────────────────────────────────

    private static async Task SeedCpus(ComputerConfigurationDBContext ctx, string folder)
    {
        var items = ReadJson(Path.Combine(folder, "cpu.json"));
        Console.WriteLine($"[Seeder] CPU: {items.Count} записей");

        foreach (var el in items)
        {
            var cpu = new Cpu
            {
                Name = S(el, "Name"),
                ComponentCategory = ComponentCategory.CPU,
                Socket = S(el, "Socket"),
                CoreCount = I(el, "CoreCount"),
                ThreadCount = I(el, "ThreadCount"),
                BaseClock = D(el, "BaseClock"),
                BoostClock = D(el, "BoostClock"),
                IntegratedGraphics = B(el, "IntegratedGraphics"),
                Series = S(el, "Series"),
                MaxMemorySpeed = I(el, "MaxMemorySpeed"),
                Tdp = I(el, "Tdp"),
            };
            FillBase(cpu, el);
            ctx.Cpus.Add(cpu);
        }
        try
        {

            await ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        Console.WriteLine("[Seeder] CPU сохранены");
    }

    // ── GPU ───────────────────────────────────────────────────────────────────

    private static async Task SeedGpus(ComputerConfigurationDBContext ctx, string folder)
    {
        var items = ReadJson(Path.Combine(folder, "gpu.json"));
        Console.WriteLine($"[Seeder] GPU: {items.Count} записей");

        foreach (var el in items)
        {
            var gpu = new Gpu
            {
                ComponentCategory = ComponentCategory.GPU,
                MemorySize = I(el, "MemorySize"),
                MemoryType = S(el, "MemoryType"),
                CoreClock = I(el, "CoreClock"),
                BoostClock = I(el, "BoostClock"),
                Tdp = I(el, "Tdp"),
                Length = I(el, "Length"),
                PowerConnectors = S(el, "PowerConnectors"),
                HdmiPorts = I(el, "HdmiPorts"),
                DisplayPorts = I(el, "DisplayPorts"),
                PcieVersion = S(el, "PcieVersion"),
                WidthSlots = I(el, "WidthSlots"),
            };
            FillBase(gpu, el);
            ctx.Gpus.Add(gpu);
        }

        await ctx.SaveChangesAsync();
        Console.WriteLine("[Seeder] GPU сохранены");
    }

    // ── Motherboard ───────────────────────────────────────────────────────────

    private static async Task SeedMotherboards(ComputerConfigurationDBContext ctx, string folder)
    {
        var items = ReadJson(Path.Combine(folder, "motherboard.json"));
        Console.WriteLine($"[Seeder] Motherboard: {items.Count} записей");

        foreach (var el in items)
        {
            var mb = new Motherboard
            {
                ComponentCategory = ComponentCategory.Motherboard,
                Socket = S(el, "Socket"),
                Chipset = S(el, "Chipset"),
                FormFactor = S(el, "FormFactor"),
                MemoryType = S(el, "MemoryType"),
                MemorySlots = I(el, "MemorySlots"),
                MaxMemory = I(el, "MaxMemory"),
                MaxMemorySpeed = I(el, "MaxMemorySpeed"),
                PcieVersion = S(el, "PcieVersion"),
                M2Slots = I(el, "M2Slots"),
                SataPorts = I(el, "SataPorts"),
                IntegratedWifi = B(el, "IntegratedWifi"),
                IntegratedBluetooth = B(el, "IntegratedBluetooth"),
                PowerConsumption = I(el, "PowerConsumption"),
            };
            FillBase(mb, el);
            ctx.Motherboards.Add(mb);
        }

        await ctx.SaveChangesAsync();
        Console.WriteLine("[Seeder] Motherboard сохранены");
    }

    // ── RAM ───────────────────────────────────────────────────────────────────

    private static async Task SeedRam(ComputerConfigurationDBContext ctx, string folder)
    {
        var items = ReadJson(Path.Combine(folder, "ram.json"));
        Console.WriteLine($"[Seeder] RAM: {items.Count} записей");

        foreach (var el in items)
        {
            var ram = new Ram
            {
                ComponentCategory = ComponentCategory.RAM,
                MemoryType = S(el, "MemoryType"),
                Capacity = I(el, "Capacity"),
                Speed = I(el, "Speed"),
                ModuleCount = I(el, "ModuleCount"),
                Timing = S(el, "Timing"),
                Voltage = D(el, "Voltage"),
            };
            FillBase(ram, el);
            ctx.Rams.Add(ram);
        }

        await ctx.SaveChangesAsync();
        Console.WriteLine("[Seeder] RAM сохранены");
    }

    // ── Storage ───────────────────────────────────────────────────────────────

    private static async Task SeedStorage(ComputerConfigurationDBContext ctx, string jsonPath)
    {
        var items = ReadJson(jsonPath);
        Console.WriteLine($"[Seeder] Storage ({Path.GetFileName(jsonPath)}): {items.Count} записей");

        foreach (var el in items)
        {
            var typeStr = S(el, "StorageType");
            var storageType = typeStr switch
            {
                "NVMe" => StorageType.NVMe,
                "SATA_SSD" => StorageType.SATA_SSD,
                "M2_SSD" => StorageType.M2_SSD,
                _ => StorageType.HDD,
            };

            var storage = new Storage
            {
                ComponentCategory = ComponentCategory.Storage,
                StorageType = storageType,
                Capacity = I(el, "Capacity"),
                Interface = S(el, "Interface"),
                ReadSpeed = I(el, "ReadSpeed"),
                WriteSpeed = I(el, "WriteSpeed"),
                FormFactor = S(el, "FormFactor"),
            };
            FillBase(storage, el);
            ctx.Storages.Add(storage);
        }

        await ctx.SaveChangesAsync();
        Console.WriteLine("[Seeder] Storage сохранены");
    }

    // ── PSU ───────────────────────────────────────────────────────────────────

    private static async Task SeedPsu(ComputerConfigurationDBContext ctx, string folder)
    {
        var items = ReadJson(Path.Combine(folder, "psu.json"));
        Console.WriteLine($"[Seeder] PSU: {items.Count} записей");

        foreach (var el in items)
        {
            var ratingStr = S(el, "EfficiencyRating");
            var rating = ratingStr switch
            {
                "Titanium" => EfficiencyRating.Titanium,
                "Platinum" => EfficiencyRating.Platinum,
                "Gold" => EfficiencyRating.Gold,
                "Silver" => EfficiencyRating.Silver,
                _ => EfficiencyRating.Bronze,
            };

            var psu = new Psu
            {
                ComponentCategory = ComponentCategory.PSU,
                Wattage = I(el, "Wattage"),
                EfficiencyRating = rating,
                Modular = B(el, "Modular"),
                SataConnectors = I(el, "SataConnectors"),
                PcieConnectors = I(el, "PcieConnectors"),
            };
            FillBase(psu, el);
            ctx.Psus.Add(psu);
        }

        await ctx.SaveChangesAsync();
        Console.WriteLine("[Seeder] PSU сохранены");
    }

    // ── Case ─────────────────────────────────────────────────────────────────

    private static async Task SeedCases(ComputerConfigurationDBContext ctx, string folder)
    {
        var items = ReadJson(Path.Combine(folder, "case.json"));
        Console.WriteLine($"[Seeder] Case: {items.Count} записей");

        foreach (var el in items)
        {
            var sizeStr = S(el, "CaseSize");
            var caseSize = sizeStr switch
            {
                "FullTower" => CaseFormFactor.FullTower,
                "MicroAtx" => CaseFormFactor.MicroAtx,
                "MiniItx" => CaseFormFactor.MiniItx,
                _ => CaseFormFactor.MidTower,
            };

            var c = new Case
            {
                ComponentCategory = ComponentCategory.Case,
                Color = S(el, "Color"),
                MaxGpuLength = I(el, "MaxGpuLength"),
                MaxCoolerHeight = I(el, "MaxCoolerHeight"),
                IncludedFans = I(el, "IncludedFans"),
                RadiatorSupport = S(el, "RadiatorSupport"),
                SidePanel = S(el, "SidePanel"),
                CaseSize = caseSize,
                SupportedMotherboardFormFactors = S(el, "SupportedMotherboardFormFactors"),
                MaxGpuWidthSlots = I(el, "MaxGpuWidthSlots"),
            };
            FillBase(c, el);
            ctx.Cases.Add(c);
        }

        await ctx.SaveChangesAsync();
        Console.WriteLine("[Seeder] Case сохранены");
    }

    // ── Cooler ────────────────────────────────────────────────────────────────

    private static async Task SeedCoolers(ComputerConfigurationDBContext ctx, string folder)
    {
        var items = ReadJson(Path.Combine(folder, "cooler.json"));
        Console.WriteLine($"[Seeder] Cooler: {items.Count} записей");

        foreach (var el in items)
        {
            var typeStr = S(el, "CoolerType");
            var coolerType = typeStr == "Liquid" ? CoolerType.Liquid : CoolerType.Air;

            int? radiatorSize = null;
            if (el.TryGetProperty("RadiatorSize", out var rsProp) &&
                rsProp.ValueKind != JsonValueKind.Null &&
                rsProp.TryGetInt32(out var rsVal) && rsVal > 0)
            {
                radiatorSize = rsVal;
            }

            var cooler = new Cooler
            {
                ComponentCategory = ComponentCategory.Cooler,
                CoolerType = coolerType,
                TdpRating = I(el, "TdpRating"),
                Height = I(el, "Height"),
                RadiatorSize = radiatorSize ?? 0,
                NoiseLevel = D(el, "NoiseLevel"),
            };
            FillBase(cooler, el);
            ctx.Coolers.Add(cooler);

            // Сокеты — отдельная таблица
            if (el.TryGetProperty("Sockets", out var socketsArr))
            {
                foreach (var sock in socketsArr.EnumerateArray())
                {
                    var socketStr = sock.GetString();
                    if (!string.IsNullOrEmpty(socketStr))
                    {
                        cooler.SocketSupport.Add(new CoolerSocket
                        {
                            Cooler = cooler,
                            Socket = socketStr
                        });
                    }
                }
            }
        }

        await ctx.SaveChangesAsync();
        Console.WriteLine("[Seeder] Cooler сохранены");
    }
    private static async Task SeedAddServices(ComputerConfigurationDBContext ctx)
    {
        // 1. Создаём услуги (без указания Id, они будут сгенерированы БД)
        var osService = new AdditionalService
        {
            Name = "Установка операционной системы",
            Description = "Установка выбранной ОС с настройкой драйверов",
            OptionType = OptionType.ListOption
        };

        var warrantyService = new AdditionalService
        {
            Name = "Гарантия",
            Description = "",
            OptionType = OptionType.MultiOption
        };

        var cableService = new AdditionalService
        {
            Name = "Менеджмент кабелей",
            Description = "",
            OptionType = OptionType.MultiOption
        };

        var testService = new AdditionalService
        {
            Name = "Проверка",
            Description = "Проверка",
            OptionType = OptionType.SingleOption
        };

        // Добавляем услуги в контекст
        await ctx.AdditionalServices.AddRangeAsync(osService, warrantyService, cableService, testService);
        await ctx.SaveChangesAsync(); // Сохраняем, чтобы получить Id для каждой услуги

        // 2. Создаём опции, используя полученные Id
        var osOptions = new List<AdditionalServiceOption>
    {
        new() { Option = "Windows 11 Домашняя", AdditionalPrice = 5000, AdditionalServiceId = osService.Id },
        new() { Option = "Windows 11 Pro", AdditionalPrice = 8000, AdditionalServiceId = osService.Id },
        new() { Option = "Windows 10 Домашняя", AdditionalPrice = 4000, AdditionalServiceId = osService.Id },
        new() { Option = "Ubuntu 22.04 LTS", AdditionalPrice = 0, AdditionalServiceId = osService.Id },
        new() { Option = "Fedora 38", AdditionalPrice = 0, AdditionalServiceId = osService.Id }
    };

        var warrantyOptions = new List<AdditionalServiceOption>
    {
        new() { Option = "1", AdditionalPrice = 2000, AdditionalServiceId = warrantyService.Id },
        new() { Option = "2", AdditionalPrice = 1500, AdditionalServiceId = warrantyService.Id },
        new() { Option = "3", AdditionalPrice = 1000, AdditionalServiceId = warrantyService.Id }
    };

        var cableOptions = new List<AdditionalServiceOption>
    {
        new() { Option = "Стандартная", AdditionalPrice = 0, AdditionalServiceId = cableService.Id },
        new() { Option = "Черные кабели", AdditionalPrice = 300, AdditionalServiceId = cableService.Id },
        new() { Option = "Полная кастомизация", AdditionalPrice = 500, AdditionalServiceId = cableService.Id }
    };

        var testOptions = new List<AdditionalServiceOption>
    {
        new() { Option = "Проверка", AdditionalPrice = 1000, AdditionalServiceId = testService.Id }
    };

        // Добавляем опции
        await ctx.AdditionalServiceOptions.AddRangeAsync(osOptions);
        await ctx.AdditionalServiceOptions.AddRangeAsync(warrantyOptions);
        await ctx.AdditionalServiceOptions.AddRangeAsync(cableOptions);
        await ctx.AdditionalServiceOptions.AddRangeAsync(testOptions);
        await ctx.SaveChangesAsync();

        Console.WriteLine("[Seeder] AdditionalServices и Options сохранены");
    }
}