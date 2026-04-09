using ComputerConfiguration.Models.Components;
using ComputerConfiguration.Models;
using ComputerConfiguration.Models.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerConfiguration.Converters;

namespace ComputerConfiguration.Filters;

public class ComponentFilterProvider : IComponentFilterProvider
{
    public IEnumerable<FilterBase> GetFilters(ComponentCategory type, IEnumerable<IComponent> allComponents)
    {
        yield return new SingleChoiceFilter { Name = "Manufacturer", DisplayName = "Производитель",
            Options = allComponents.Select(c => c.Manufacturer).Distinct().ToObservableCollectionObject() };
        yield return new RangeFilter { Name = "BasePrice", DisplayName = "Цена",
            Min = allComponents.Min(c => c.BasePrice), Max = allComponents.Max(c => c.BasePrice)};

        switch (type)
        {
            case ComponentCategory.CPU:
                yield return new MultiChoiceFilter { Name = "CoreCount", DisplayName = "Количество ядер",
                    Options = allComponents
        .OfType<Cpu>()
        .Select(c => (object)c.CoreCount) 
        .Distinct()
        .ToObservableCollectionObject()
                };
                //yield return new ChoiceFilter { Name = "Socket", DisplayName = "Сокет", Options = new ObservableCollection<object>(allComponents.Select(c => (c as Cpu)?.Socket).Distinct()) };
                //yield return new RangeFilter { Name = "BasePrice", DisplayName = "Цена", Min = 0, Max = allComponents.Max(c => c.BasePrice), CurrentMin = 0, CurrentMax = allComponents.Max(c => c.BasePrice) };
                break;
            //case ComponentCategory.Motherboard:
            //    yield return new TextFilter { Name = "Manufacturer", DisplayName = "Производитель" };
            //    yield return new ChoiceFilter { Name = "Socket", DisplayName = "Сокет", Options = new ObservableCollection<object>(allComponents.Select(c => (c as Motherboard)?.Socket).Distinct()) };
            //    yield return new ChoiceFilter { Name = "FormFactor", DisplayName = "Форм-фактор", Options = new ObservableCollection<object>(allComponents.Select(c => (c as Motherboard)?.FormFactor).Distinct()) };
            //    break;
        }
    }
}
