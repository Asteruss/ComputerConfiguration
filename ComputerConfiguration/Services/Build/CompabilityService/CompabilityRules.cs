using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Enums;

namespace ComputerConfiguration.Services.Build.Compability;


public class CpuMotherboardSocketRule : ICompatibilityRule
{
    public CompatibilityRuleResult Check(ComputerBuild build)
    {
        if (build.Motherboard == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Материнская плата не установлена.");
        if (build.Cpu == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Процессор не установлен.");
        if (build.Cpu.Socket != build.Motherboard.Socket)
            return new(CompatibilityRuleEnum.Error, $"Сокет процессора ({build.Cpu.Socket}) не совместим с сокетом материнской платы ({build.Motherboard.Socket})");
        return new();
    }
}

public class CpuCoolerTdpRule : ICompatibilityRule
{
    public CompatibilityRuleResult Check(ComputerBuild build)
    {
        if (build.Cooler == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Система охлаждения не установлена.");
        if (build.Cpu == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Процессор не установлен.");
        if (build.Cpu.Tdp > build.Cooler.TdpRating)
            return new(CompatibilityRuleEnum.Error, $"TDP процессора ({build.Cpu.Tdp} Вт) превышает максимальный TDP кулера ({build.Cooler.TdpRating} Вт)");
        return new();
    }
}

public class RamCpuSpeedRule : ICompatibilityRule
{
    public CompatibilityRuleResult Check(ComputerBuild build)
    {
        if (build.Rams == null || !build.Rams.Any())
            return new(CompatibilityRuleEnum.ComponentNotFound, "Оперативная память не установлена.");
        if (build.Cpu == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Процессор не установлен.");
        var maxSpeed = build.Rams.Max(r => r.Speed);
        if (maxSpeed > build.Cpu.MaxMemorySpeed)
            return new(CompatibilityRuleEnum.Warning, $"Частота RAM ({maxSpeed} МГц) превышает максимальную частоту, поддерживаемую процессором ({build.Cpu.MaxMemorySpeed} МГц). Память будет работать на пониженной частоте.");
        return new();
    }
}

public class RamMotherboardTypeRule : ICompatibilityRule
{
    public CompatibilityRuleResult Check(ComputerBuild build)
    {
        if (build.Motherboard == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Материнская плата не установлена.");
        if (build.Rams == null || !build.Rams.Any())
            return new(CompatibilityRuleEnum.ComponentNotFound, "Оперативная память не установлена.");
        foreach (var ram in build.Rams)
        {
            if (ram.MemoryType != build.Motherboard.MemoryType)
                return new(CompatibilityRuleEnum.Error, $"Тип памяти RAM '{ram.MemoryType}' не поддерживается материнской платой (требуется '{build.Motherboard.MemoryType}')");
        }
        return new();
    }
}

public class RamMotherboardCapacityRule : ICompatibilityRule
{
    public CompatibilityRuleResult Check(ComputerBuild build)
    {
        if (build.Motherboard == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Материнская плата не установлена.");
        if (build.Rams == null || !build.Rams.Any())
            return new(CompatibilityRuleEnum.ComponentNotFound, "Оперативная память не установлена.");
        var totalCapacity = build.Rams.Sum(r => r.TotalCapacity);
        if (totalCapacity > build.Motherboard.MaxMemory)
            return new(CompatibilityRuleEnum.Error, $"Общий объём RAM ({totalCapacity} ГБ) превышает максимальный объём материнской платы ({build.Motherboard.MaxMemory} ГБ)");
        return new();
    }
}

public class RamMotherboardSpeedRule : ICompatibilityRule
{
    public CompatibilityRuleResult Check(ComputerBuild build)
    {
        if (build.Motherboard == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Материнская плата не установлена.");
        if (build.Rams == null || !build.Rams.Any())
            return new(CompatibilityRuleEnum.ComponentNotFound, "Оперативная память не установлена.");
        var maxSpeed = build.Rams.Max(r => r.Speed);
        if (maxSpeed > build.Motherboard.MaxMemorySpeed)
            return new(CompatibilityRuleEnum.Warning, $"Максимальная частота RAM ({maxSpeed} МГц) выше поддерживаемой материнской платой ({build.Motherboard.MaxMemorySpeed} МГц). Память будет работать на пониженной частоте.");
        return new();
    }
}

public class RamMotherboardSlotsRule : ICompatibilityRule
{
    public CompatibilityRuleResult Check(ComputerBuild build)
    {
        if (build.Motherboard == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Материнская плата не установлена.");
        if (build.Rams == null || !build.Rams.Any())
            return new(CompatibilityRuleEnum.ComponentNotFound, "Оперативная память не установлена.");
        var totalModules = build.Rams.Sum(r => r.ModuleCount);
        if (totalModules > build.Motherboard.MemorySlots)
            return new(CompatibilityRuleEnum.Error, $"Общее количество модулей RAM ({totalModules}) превышает число слотов на материнской плате ({build.Motherboard.MemorySlots})");
        return new();
    }
}

public class StorageMotherboardM2CountRule : ICompatibilityRule
{
    public CompatibilityRuleResult Check(ComputerBuild build)
    {
        if (build.Motherboard == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Материнская плата не установлена.");
        if (build.Storages == null || !build.Storages.Any())
            return new(CompatibilityRuleEnum.ComponentNotFound, "Накопители не установлены.");
        var m2Count = build.Storages.Count(s => s.StorageType == StorageType.M2_SSD);
        if (m2Count > build.Motherboard.M2Slots)
            return new(CompatibilityRuleEnum.Error, $"Недостаточно M.2 слотов на материнской плате (требуется {m2Count}, доступно {build.Motherboard.M2Slots})");
        return new();
    }
}

public class StorageMotherboardSataCountRule : ICompatibilityRule
{
    public CompatibilityRuleResult Check(ComputerBuild build)
    {
        if (build.Motherboard == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Материнская плата не установлена.");
        if (build.Storages == null || !build.Storages.Any())
            return new(CompatibilityRuleEnum.ComponentNotFound, "Накопители не установлены.");
        var sataCount = build.Storages.Count(s => s.StorageType == StorageType.SATA_SSD);
        if (sataCount > build.Motherboard.SataPorts)
            return new(CompatibilityRuleEnum.Error, $"Недостаточно SATA портов на материнской плате (требуется {sataCount}, доступно {build.Motherboard.SataPorts})");
        return new();
    }
}

public class StorageM2InterfaceWarningRule : ICompatibilityRule
{
    public CompatibilityRuleResult Check(ComputerBuild build)
    {
        if (build.Motherboard == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Материнская плата не установлена.");
        if (build.Storages == null || !build.Storages.Any())
            return new(CompatibilityRuleEnum.ComponentNotFound, "Накопители не установлены.");
        var m2Storages = build.Storages.Where(s => s.StorageType == StorageType.M2_SSD);
        bool hasIssue = false;
        foreach (var storage in m2Storages)
        {
            if (storage.Interface.Contains("NVMe") && build.Motherboard.PcieVersion == null) // упрощённая проверка
                hasIssue = true;
        }
        if (hasIssue)
            return new(CompatibilityRuleEnum.Warning, "M.2 накопитель с интерфейсом NVMe может не работать в некоторых M.2 слотах. Проверьте спецификации материнской платы.");
        return new();
    }
}

public class GpuCaseLengthRule : ICompatibilityRule
{
    public CompatibilityRuleResult Check(ComputerBuild build)
    {
        if (build.Case == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Корпус не установлен.");
        if (build.Gpu == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Видеокарта не установлена.");
        if (build.Gpu.Length > build.Case.MaxGpuLength)
            return new(CompatibilityRuleEnum.Error, $"Видеокарта слишком длинная ({build.Gpu.Length} мм) для корпуса (макс. {build.Case.MaxGpuLength} мм)");
        return new();
    }
}

public class GpuCaseWidthSlotsWarningRule : ICompatibilityRule
{
    public CompatibilityRuleResult Check(ComputerBuild build)
    {
        if (build.Case == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Корпус не установлен.");
        if (build.Gpu == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Видеокарта не установлена.");
        if (build.Gpu.WidthSlots > build.Case.MaxGpuWidthSlots && build.Case.MaxGpuWidthSlots > 0)
            return new(CompatibilityRuleEnum.Warning, $"Видеокарта занимает {build.Gpu.WidthSlots} слота, но корпус поддерживает максимум {build.Case.MaxGpuWidthSlots} слота (возможны проблемы с установкой)");
        return new();
    }
}

public class GpuPsuPowerConnectorsRule : ICompatibilityRule
{
    public CompatibilityRuleResult Check(ComputerBuild build)
    {
        if (build.Psu == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Блок питания не установлен.");
        if (build.Gpu == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Видеокарта не установлена.");
        // Парсим требуемые разъёмы из строки, например "2x8-pin"
        int required = 0;
        var parts = build.Gpu.PowerConnectors?.Split('x');
        if (parts != null && parts.Length == 2 && int.TryParse(parts[0], out int count))
            required = count;
        if (required > build.Psu.PcieConnectors)
            return new(CompatibilityRuleEnum.Error, $"Недостаточно PCIe разъёмов на блоке питания (требуется {required}, доступно {build.Psu.PcieConnectors})");
        return new();
    }
}

public class TotalPowerConsumptionRule : ICompatibilityRule
{
    public CompatibilityRuleResult Check(ComputerBuild build)
    {
        if (build.Psu == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Блок питания не установлен.");
        int total = 0;
        if (build.Cpu != null) total += build.Cpu.Tdp;
        if (build.Gpu != null) total += build.Gpu.Tdp;
        if (build.Motherboard != null) total += build.Motherboard.PowerConsumption;
        if (build.Rams != null) total += build.Rams.Sum(r => 5 * r.ModuleCount); // 5 Вт на модуль
        if (build.Storages != null) total += build.Storages.Sum(s => 10); // 10 Вт на накопитель
        if (build.Cooler != null) total += 10; // приблизительно

        var required = (int)(total * 1.2);
        if (build.Psu.Wattage < required)
            return new(CompatibilityRuleEnum.Error, $"Мощность блока питания ({build.Psu.Wattage} Вт) недостаточна (требуется минимум {required} Вт)");
        return new();
    }
}

public class StoragePsuSataConnectorsRule : ICompatibilityRule
{
    public CompatibilityRuleResult Check(ComputerBuild build)
    {
        if (build.Psu == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Блок питания не установлен.");
        if (build.Storages == null || !build.Storages.Any())
            return new(CompatibilityRuleEnum.ComponentNotFound, "Накопители не установлены.");
        var sataCount = build.Storages.Count(s => s.StorageType == StorageType.SATA_SSD);
        if (sataCount > build.Psu.SataConnectors)
            return new(CompatibilityRuleEnum.Error, $"Недостаточно SATA разъёмов питания (требуется {sataCount}, доступно {build.Psu.SataConnectors})");
        return new();
    }
}

public class MotherboardCaseFormFactorRule : ICompatibilityRule
{
    public CompatibilityRuleResult Check(ComputerBuild build)
    {
        if (build.Case == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Корпус не установлен.");
        if (build.Motherboard == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Материнская плата не установлена.");
        if (!build.Case.SupportedMotherboardFormFactors.Contains(build.Motherboard.FormFactor))
            return new(CompatibilityRuleEnum.Error, $"Форм-фактор материнской платы {build.Motherboard.FormFactor} не поддерживается корпусом (поддерживает: {build.Case.SupportedMotherboardFormFactors})");
        return new();
    }
}

public class CoolerCaseAirHeightRule : ICompatibilityRule
{
    public CompatibilityRuleResult Check(ComputerBuild build)
    {
        if (build.Case == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Корпус не установлен.");
        if (build.Cooler == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Кулер не установлен.");
        if (build.Cooler.CoolerType != CoolerType.Air)
            return new(); // правило только для воздушных
        if (build.Cooler.Height > build.Case.MaxCoolerHeight)
            return new(CompatibilityRuleEnum.Error, $"Высота кулера ({build.Cooler.Height} мм) превышает максимальную высоту для корпуса ({build.Case.MaxCoolerHeight} мм)");
        return new();
    }
}

public class CoolerCaseLiquidRadiatorRule : ICompatibilityRule
{
    public CompatibilityRuleResult Check(ComputerBuild build)
    {
        if (build.Case == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Корпус не установлен.");
        if (build.Cooler == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Кулер не установлен.");
        if (build.Cooler.CoolerType != CoolerType.Liquid)
            return new();
        var supportedSizes = build.Case.RadiatorSupport?.Split(',').Select(s => s.Trim()).Select(int.Parse).ToList();
        if (supportedSizes == null || !supportedSizes.Contains(build.Cooler.RadiatorSize))
            return new(CompatibilityRuleEnum.Error, $"Размер радиатора СЖО {build.Cooler.RadiatorSize} мм не поддерживается корпусом (поддерживает: {build.Case.RadiatorSupport})");
        return new();
    }
}

public class CoolerCpuSocketSupportRule : ICompatibilityRule
{
    public CompatibilityRuleResult Check(ComputerBuild build)
    {
        if (build.Cooler == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Кулер не установлен.");
        if (build.Cpu == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Процессор не установлен.");
        if (!build.Cooler.SocketSupport.Any(s => s.Socket == build.Cpu.Socket))
            return new(CompatibilityRuleEnum.Error, $"Кулер не поддерживает сокет процессора {build.Cpu.Socket}");
        return new();
    }
}

public class GpuMotherboardPcieVersionWarningRule : ICompatibilityRule
{
    public CompatibilityRuleResult Check(ComputerBuild build)
    {
        if (build.Motherboard == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Материнская плата не установлена.");
        if (build.Gpu == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Видеокарта не установлена.");
        var gpuVer = double.TryParse(build.Gpu.PcieVersion, out double gv) ? gv : 0;
        var mbVer = double.TryParse(build.Motherboard.PcieVersion, out double mv) ? mv : 0;
        if (gpuVer > mbVer)
            return new(CompatibilityRuleEnum.Warning, $"Версия PCIe видеокарты ({build.Gpu.PcieVersion}) выше, чем у материнской платы ({build.Motherboard.PcieVersion}). Карта будет работать, но с ограничением пропускной способности.");
        return new();
    }
}

public class CpuGpuPriceBalanceWarningRule : ICompatibilityRule
{
    public CompatibilityRuleResult Check(ComputerBuild build)
    {
        if (build.Cpu == null || build.Gpu == null)
            return new(CompatibilityRuleEnum.ComponentNotFound, "Процессор или видеокарта не установлены.");
        if (build.Cpu.BasePrice < 0.5 * build.Gpu.BasePrice)
            return new(CompatibilityRuleEnum.Warning, $"Процессор значительно дешевле видеокарты, возможен bottleneck (узкое место) в играх.");
        return new();
    }
}

public class RamSameTypeRule : ICompatibilityRule
{
    public CompatibilityRuleResult Check(ComputerBuild build)
    {
        if (build.Rams == null || build.Rams.Count <= 1)
            return new();
        var firstType = build.Rams.First().MemoryType;
        if (build.Rams.Any(r => r.MemoryType != firstType))
            return new(CompatibilityRuleEnum.Error, "Все модули оперативной памяти должны быть одного типа (например, DDR4 или DDR5)");
        return new();
    }
}
