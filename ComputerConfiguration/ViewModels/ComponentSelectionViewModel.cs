using ComputerConfiguration.Builder;
using ComputerConfiguration.Commands;
using ComputerConfiguration.Converters;
using ComputerConfiguration.Filters;
using ComputerConfiguration.Models;
using ComputerConfiguration.Models.Components;
using ComputerConfiguration.Models.Enums;
using System.Collections.ObjectModel;

namespace ComputerConfiguration.ViewModels;

public class ComponentSelectionViewModel : ViewModelBase
{
    public List<IComponent> _components { get; set; }
    public ObservableCollection<IComponent> FilteredComponents { get; set; }
    public ObservableCollection<FilterBase> Filters { get; } = new();
    private string _lastSortField;
    private bool _ascending = true;
    private readonly ComponentCategory _category;
    private readonly IComputerBuildDtoBuilder _builder;
    private RelayCommand _sortBy;
    public RelayCommand SortBy
    {
        get => _sortBy ?? (_sortBy = new RelayCommand((param) =>
        {
            string? field = param as string;
            if (string.IsNullOrEmpty(field)) return;

            if (_lastSortField == field)
                _ascending = !_ascending;
            else
            {
                _lastSortField = field;
                _ascending = true;
            }

            var sorted = FilteredComponents.ToList();
            switch (field)
            {
                case "Name":
                    if (_ascending)
                        sorted = [.. sorted.OrderBy(c => c.Name)];
                    else
                        sorted = [.. sorted.OrderByDescending(c => c.Name)];
                    break;
                case "Price":
                    if (_ascending)
                        sorted = [.. sorted.OrderBy(c => c.BasePrice)];
                    else
                        sorted = [.. sorted.OrderByDescending(c => c.BasePrice)];
                    break;
                case "Rating":
                    if (_ascending)
                        sorted = [.. sorted.OrderBy(c => c.Rating)];
                    else
                        sorted = [.. sorted.OrderByDescending(c => c.Rating)];
                    break;
                default:
                    return;
            }

            FilteredComponents = sorted.ToObservableCollection();
            OnPropertyChanged(nameof(FilteredComponents));
        }));
    }

    private RelayCommand _selectCommand;
    public RelayCommand SelectCommand
    {
        get => _selectCommand ??= new((component) =>
        {
            if (component is Cpu cpu)
                _builder.SetCpu(cpu);
            if (component is Gpu gpu)
                _builder.SetGpu(gpu);
            if (component is Motherboard mot)
                _builder.SetMotherboard(mot);
            if (component is Ram ram)
                _builder.AddRam(ram);
            if (component is Cooler cooler)
                _builder.SetCooler(cooler);
            if (component is Psu psu)
                _builder.SetPsu(psu);
            if (component is Case case_)
                _builder.SetCase(case_);
            if (component is Storage storage)
                _builder.AddStorage(storage);
        });
    }

    private RelayCommand _selectFakeCommand;
    public RelayCommand SelectFakeCommand
    {
        get => _selectFakeCommand ??= new((component) =>
        {
            if (component is Cpu cpu)
                _builder.SetCpu(cpu, ComponentStatus.SelectedAsFake);
            if (component is Gpu gpu)
                _builder.SetGpu(gpu, ComponentStatus.SelectedAsFake);
            if (component is Motherboard mot)
                _builder.SetMotherboard(mot, ComponentStatus.SelectedAsFake);
            if (component is Ram ram)
                _builder.AddRam(ram, ComponentStatus.SelectedManyAsFake);
            if (component is Cooler cooler)
                _builder.SetCooler(cooler, ComponentStatus.SelectedAsFake);
            if (component is Psu psu)
                _builder.SetPsu(psu, ComponentStatus.SelectedAsFake);
            if (component is Case case_)
                _builder.SetCase(case_, ComponentStatus.SelectedAsFake);
            if (component is Storage storage)
                _builder.AddStorage(storage, ComponentStatus.SelectedManyAsFake);

        });
    }
    private RelayCommand _removeCommand;
    public RelayCommand RemoveCommand
    {
        get => _removeCommand ??= new((component) =>
        {
            if (component is Cpu cpu)
                _builder.RemoveCpu(cpu);
            if (component is Gpu gpu)
                _builder.RemoveGpu(gpu);
            if (component is Motherboard mot)
                _builder.RemoveMotherboard(mot);
            if (component is Ram ram)
                _builder.ClearRams(ram);
            if (component is Cooler cooler)
                _builder.RemoveCooler(cooler);
            if (component is Psu psu)
                _builder.RemovePsu(psu);
            if (component is Case case_)
                _builder.RemoveCase(case_);
            if (component is Storage storage)
                _builder.ClearStorages(storage);
        });
    }
    private RelayCommand _removeFakeCommand;
    public RelayCommand RemoveFakeCommand
    {
        get => _removeFakeCommand ??= new((component) =>
        {
            if (component is Ram ram)
                _builder.ClearRams(ram, ComponentStatus.SelectedManyAsFake);
            if (component is Storage storage)
                _builder.ClearStorages(storage, ComponentStatus.SelectedManyAsFake);
        });
    }
    public ComponentSelectionViewModel(IEnumerable<IComponent> components, IComponentFilterProvider filterProvider,
        ComponentCategory category, IComputerBuildDtoBuilder builder)
    {
        _components = [.. components];
        FilteredComponents = _components.ToObservableCollection();
        Filters = filterProvider.GetFilters(category, components).ToObservableCollection();
        OnPropertyChanged(nameof(Filters));
        _category = category;

        foreach (var filter in Filters)
            filter.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(FilterBase.Value) || e.PropertyName == nameof(MultiChoiceFilter.SelectedItems) ||
                e.PropertyName == nameof(RangeFilter.CurrentMin) || e.PropertyName == nameof(RangeFilter.CurrentMax))
                    _applyFilters();
            };
        _builder = builder;


    }
    private void _applyFilters()
    {
        var filtered = _components;
        foreach (var filter in Filters)
            filtered = [.. filtered.Where(c => filter.Matches(c))];
        FilteredComponents = filtered.ToObservableCollection();
        OnPropertyChanged(nameof(FilteredComponents));

    }
}
