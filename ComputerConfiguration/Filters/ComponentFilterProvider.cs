using ComputerConfiguration.Converters;
using ComputerConfiguration.Filters.Strategies;
using ComputerConfiguration.Models;
using ComputerConfiguration.Models.Components;
using ComputerConfiguration.Models.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Filters;

public class ComponentFilterProvider : IComponentFilterProvider
{
    public IEnumerable<FilterBase> GetFilters(ComponentCategory type, IEnumerable<IComponent> allComponents)
    {
        // Общие фильтры для всех компонентов (если нужны)
        yield return new SingleChoiceFilter
        {
            Name = "Manufacturer",
            DisplayName = "Производитель",
            MatchStrategy = new ExactMatchStrategy(),
            Options = allComponents.Select(c => c.Manufacturer).Distinct().ToObservableCollectionObject()
        };
        yield return new RangeFilter
        {
            Name = "BasePrice",
            DisplayName = "Цена",
            MatchStrategy = new RangeMatchStrategy(),
            Min = allComponents.Min(c => c.BasePrice),
            Max = allComponents.Max(c => c.BasePrice)
        };

        // Фильтр по названию (текст) – частичное совпадение
        yield return new TextFilter
        {
            Name = "Name",
            DisplayName = "Название",
            MatchStrategy = new ContainsMatchStrategy()
        };

        yield return new FavoriteOnlyFilter
        {
            Name = "Name",
            DisplayName = "Только избранные"
        };

        switch (type)
        {
            case ComponentCategory.CPU:
                // Выбор по сокету
                yield return new SingleChoiceFilter
                {
                    Name = "Socket",
                    DisplayName = "Сокет",
                    MatchStrategy = new ExactMatchStrategy(),
                    Options = allComponents.OfType<Cpu>().Select(c => c.Socket).Distinct().ToObservableCollectionObject()
                };
                // Количество ядер (множественный выбор)
                yield return new MultiChoiceFilter
                {
                    Name = "CoreCount",
                    DisplayName = "Количество ядер",
                    MatchStrategy = new ExactMatchStrategy(),
                    Options = allComponents.OfType<Cpu>().Select(c => (object)c.CoreCount).Distinct().ToObservableCollectionObject()
                };
                // TDP (диапазон)
                yield return new RangeFilter
                {
                    Name = "Tdp",
                    DisplayName = "TDP (Вт)",
                    MatchStrategy = new RangeMatchStrategy(),
                    Min = allComponents.OfType<Cpu>().Min(c => c.Tdp),
                    Max = allComponents.OfType<Cpu>().Max(c => c.Tdp)
                };
                break;

            case ComponentCategory.Motherboard:
                // Сокет
                yield return new SingleChoiceFilter
                {
                    Name = "Socket",
                    DisplayName = "Сокет",
                    MatchStrategy = new ExactMatchStrategy(),
                    Options = allComponents.OfType<Motherboard>().Select(mb => mb.Socket).Distinct().ToObservableCollectionObject()
                };
                // Форм-фактор
                yield return new SingleChoiceFilter
                {
                    Name = "FormFactor",
                    DisplayName = "Форм-фактор",
                    MatchStrategy = new ExactMatchStrategy(),
                    Options = allComponents.OfType<Motherboard>().Select(mb => mb.FormFactor).Distinct().ToObservableCollectionObject()
                };
                // Тип памяти
                yield return new SingleChoiceFilter
                {
                    Name = "MemoryType",
                    DisplayName = "Тип памяти",
                    MatchStrategy = new ExactMatchStrategy(),
                    Options = allComponents.OfType<Motherboard>().Select(mb => mb.MemoryType).Distinct().ToObservableCollectionObject()
                };
                // Количество слотов памяти
                yield return new MultiChoiceFilter
                {
                    Name = "MemorySlots",
                    DisplayName = "Слоты памяти",
                    MatchStrategy = new ExactMatchStrategy(),
                    Options = allComponents.OfType<Motherboard>().Select(mb => (object)mb.MemorySlots).Distinct().ToObservableCollectionObject()
                };
                // Максимальный объём памяти
                yield return new RangeFilter
                {
                    Name = "MaxMemory",
                    DisplayName = "Макс. память (ГБ)",
                    MatchStrategy = new RangeMatchStrategy(),
                    Min = allComponents.OfType<Motherboard>().Min(mb => mb.MaxMemory),
                    Max = allComponents.OfType<Motherboard>().Max(mb => mb.MaxMemory)
                };
                // PCIe версия
                yield return new SingleChoiceFilter
                {
                    Name = "PcieVersion",
                    DisplayName = "PCIe версия",
                    MatchStrategy = new ExactMatchStrategy(),
                    Options = allComponents.OfType<Motherboard>().Select(mb => mb.PcieVersion).Distinct().ToObservableCollectionObject()
                };
                break;

            case ComponentCategory.GPU:
                // Объём видеопамяти
                yield return new MultiChoiceFilter
                {
                    Name = "MemorySize",
                    DisplayName = "Видеопамять (ГБ)",
                    MatchStrategy = new ExactMatchStrategy(),
                    Options = allComponents.OfType<Gpu>().Select(gpu => (object)gpu.MemorySize).Distinct().ToObservableCollectionObject()
                };
                // TDP видеокарты
                yield return new RangeFilter
                {
                    Name = "Tdp",
                    DisplayName = "TDP (Вт)",
                    MatchStrategy = new RangeMatchStrategy(),
                    Min = allComponents.OfType<Gpu>().Min(gpu => gpu.Tdp),
                    Max = allComponents.OfType<Gpu>().Max(gpu => gpu.Tdp)
                };
                // Длина видеокарты
                yield return new RangeFilter
                {
                    Name = "Length",
                    DisplayName = "Длина (мм)",
                    MatchStrategy = new RangeMatchStrategy(),
                    Min = allComponents.OfType<Gpu>().Min(gpu => gpu.Length),
                    Max = allComponents.OfType<Gpu>().Max(gpu => gpu.Length)
                };
                break;

            case ComponentCategory.RAM:
                // Тип памяти (DDR4/DDR5)
                yield return new SingleChoiceFilter
                {
                    Name = "MemoryType",
                    DisplayName = "Тип памяти",
                    MatchStrategy = new ExactMatchStrategy(),
                    Options = allComponents.OfType<Ram>().Select(ram => ram.MemoryType).Distinct().ToObservableCollectionObject()
                };
                // Объём одного модуля
                yield return new MultiChoiceFilter
                {
                    Name = "Capacity",
                    DisplayName = "Объём модуля (ГБ)",
                    MatchStrategy = new ExactMatchStrategy(),
                    Options = allComponents.OfType<Ram>().Select(ram => (object)ram.Capacity).Distinct().ToObservableCollectionObject()
                };
                // Частота
                yield return new RangeFilter
                {
                    Name = "Speed",
                    DisplayName = "Частота (МГц)",
                    MatchStrategy = new RangeMatchStrategy(),
                    Min = allComponents.OfType<Ram>().Min(ram => ram.Speed),
                    Max = allComponents.OfType<Ram>().Max(ram => ram.Speed)
                };
                break;

            case ComponentCategory.Storage:
                // Тип накопителя (SSD, HDD, NVMe, SATA)
                yield return new MultiChoiceFilter
                {
                    Name = "StorageType",
                    DisplayName = "Тип",
                    MatchStrategy = new ExactMatchStrategy(),
                    Options = allComponents.OfType<Storage>().Select(s => (object)s.StorageType).Distinct().ToObservableCollectionObject()
                };
                // Ёмкость
                yield return new MultiChoiceFilter
                {
                    Name = "Capacity",
                    DisplayName = "Ёмкость (ГБ)",
                    MatchStrategy = new ExactMatchStrategy(),
                    Options = allComponents.OfType<Storage>().Select(s => (object)s.Capacity).Distinct().ToObservableCollectionObject()
                };
                // Интерфейс (SATA, NVMe, ...)
                yield return new SingleChoiceFilter
                {
                    Name = "Interface",
                    DisplayName = "Интерфейс",
                    MatchStrategy = new ExactMatchStrategy(),
                    Options = allComponents.OfType<Storage>().Select(s => s.Interface).Distinct().ToObservableCollectionObject()
                };
                break;

            case ComponentCategory.PSU:
                // Мощность
                yield return new RangeFilter
                {
                    Name = "Wattage",
                    DisplayName = "Мощность (Вт)",
                    MatchStrategy = new RangeMatchStrategy(),
                    Min = allComponents.OfType<Psu>().Min(psu => psu.Wattage),
                    Max = allComponents.OfType<Psu>().Max(psu => psu.Wattage)
                };
                // Сертификат эффективности
                yield return new SingleChoiceFilter
                {
                    Name = "EfficiencyRating",
                    DisplayName = "Сертификат",
                    MatchStrategy = new ExactMatchStrategy(),
                    Options = allComponents.OfType<Psu>().Select(psu => (object)psu.EfficiencyRating).Distinct().ToObservableCollectionObject()
                };
                break;

            case ComponentCategory.Case:
                // Форм-фактор корпуса
                yield return new SingleChoiceFilter
                {
                    Name = "SupportedMotherboardFormFactors",
                    DisplayName = "Поддержка форм‑фактора",
                    MatchStrategy = new ContainsMatchStrategy(), // т.к. строка может содержать несколько форматов через запятую
                    Options = allComponents.OfType<Case>().Select(c => c.SupportedMotherboardFormFactors).Distinct().ToObservableCollectionObject()
                };
                // Максимальная длина видеокарты
                yield return new RangeFilter
                {
                    Name = "MaxGpuLength",
                    DisplayName = "Макс. длина GPU (мм)",
                    MatchStrategy = new RangeMatchStrategy(),
                    Min = allComponents.OfType<Case>().Min(c => c.MaxGpuLength),
                    Max = allComponents.OfType<Case>().Max(c => c.MaxGpuLength)
                };
                // Максимальная высота кулера
                yield return new RangeFilter
                {
                    Name = "MaxCoolerHeight",
                    DisplayName = "Макс. высота кулера (мм)",
                    MatchStrategy = new RangeMatchStrategy(),
                    Min = allComponents.OfType<Case>().Min(c => c.MaxCoolerHeight),
                    Max = allComponents.OfType<Case>().Max(c => c.MaxCoolerHeight)
                };
                break;

            case ComponentCategory.Cooler:
                // Тип охлаждения (Air/Liquid)
                yield return new SingleChoiceFilter
                {
                    Name = "CoolerType",
                    DisplayName = "Тип",
                    MatchStrategy = new ExactMatchStrategy(),
                    Options = allComponents.OfType<Cooler>().Select(c => (object)c.CoolerType).Distinct().ToObservableCollectionObject()
                };
                // TDP кулера
                yield return new RangeFilter
                {
                    Name = "TdpRating",
                    DisplayName = "TDP рассеивания (Вт)",
                    MatchStrategy = new RangeMatchStrategy(),
                    Min = allComponents.OfType<Cooler>().Min(c => c.TdpRating),
                    Max = allComponents.OfType<Cooler>().Max(c => c.TdpRating)
                };
                // Высота (для воздушных) – будет показана всегда, но для жидкостных высота не критична, оставим
                yield return new RangeFilter
                {
                    Name = "Height",
                    DisplayName = "Высота (мм)",
                    MatchStrategy = new RangeMatchStrategy(),
                    Min = allComponents.OfType<Cooler>().Min(c => c.Height),
                    Max = allComponents.OfType<Cooler>().Max(c => c.Height)
                };
                // Размер радиатора (для жидкостных)
                yield return new MultiChoiceFilter
                {
                    Name = "RadiatorSize",
                    DisplayName = "Размер радиатора (мм)",
                    MatchStrategy = new ExactMatchStrategy(),
                    Options = allComponents.OfType<Cooler>().Where(c => c.RadiatorSize > 0).Select(c => (object)c.RadiatorSize).Distinct().ToObservableCollectionObject()
                };
                break;
        }
    }
}
