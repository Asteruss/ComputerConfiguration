using ComputerConfiguration.Builder;
using ComputerConfiguration.Commands;
using ComputerConfiguration.Converters;
using ComputerConfiguration.Filters;
using ComputerConfiguration.Models;
using ComputerConfiguration.Models.Components;
using ComputerConfiguration.Models.Enums;
using ComputerConfiguration.Services.Authentication;
using ComputerConfiguration.Services.Favorites;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.Globalization;

namespace ComputerConfiguration.ViewModels;

public class ComponentSelectionViewModel : ViewModelBase
{
    public List<ComponentBase> _components { get; set; }
    public ObservableCollection<ComponentBase> FilteredComponents { get; set; }
    public ObservableCollection<FilterBase> Filters { get; } = new();
    public ObservableCollection<SortOptionViewModel> SortOptions { get; set; }
    private readonly ComponentCategory _category;
    private readonly IComputerBuildDtoBuilder _builder;
    private readonly IComponentEnricher _enricher;
    private readonly FavoriteFacade _favoriteFacade;
    private RelayCommand _sortBy;
    public RelayCommand SortBy
    {
        get => _sortBy ?? (_sortBy = new RelayCommand((option) =>
        {
            if (option is SortOptionViewModel opt)
            {

                opt.IsAscending = !opt.IsAscending;
                _components = opt.Sort()(_components).ToList();
                FilteredComponents = opt.Sort()(FilteredComponents).ToObservableCollection();
                OnPropertyChanged(nameof(FilteredComponents));
            }
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

    private RelayCommand _addFavorite;
    public RelayCommand AddFavorite
    {
        get => _addFavorite ??= new(async (component) =>
        {
            if (component is ComponentBase cmp)
            {
                await _favoriteFacade.AddFavoriteAsync(cmp.Id, cmp.ComponentCategory);
                cmp.IsFavorite = true;
            }
        });
    }
    private RelayCommand _deleteFavorite;
    public RelayCommand DeleteFavorite
    {
        get => _deleteFavorite ??= new(async (component) =>
        {
            if (component is ComponentBase cmp)
            {
                await _favoriteFacade.RemoveFavoriteAsync(cmp.Id, cmp.ComponentCategory);
                cmp.IsFavorite = false;
            }
        });
    }

    public ComponentSelectionViewModel(IEnumerable<ComponentBase> components, IComponentFilterProvider filterProvider,
        ComponentCategory category, IComputerBuildDtoBuilder builder, FavoriteFacade favoriteFacade, IComponentEnricher enricher,
        IAuthService authService)
    {
        OnPropertyChanged(nameof(Filters));
        _category = category;
        _favoriteFacade = favoriteFacade;
        _enricher = enricher;

        _components = [.. components];
        _ = LoadDataAsync();
        authService.UserChanged += () => _ = LoadDataAsync();
        Filters = filterProvider.GetFilters(category, components).ToObservableCollection();

        foreach (var filter in Filters)
            filter.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(FilterBase.Value) || e.PropertyName == nameof(MultiChoiceFilter.SelectedItems) ||
                    e.PropertyName == nameof(RangeFilter.CurrentMin) || e.PropertyName == nameof(RangeFilter.CurrentMax) ||
                    e.PropertyName == nameof(FavoriteOnlyFilter.IsEnabled))
                    _applyFilters();
            };
        _builder = builder;

        SortOptions = new ObservableCollection<SortOptionViewModel>
    {
        new SortOptionViewModel("Сортировать по имени",
            items => items.OrderBy(c => c.Name),
            items => items.OrderByDescending(c => c.Name)),
        new SortOptionViewModel("Сортировать по цене",
            items => items.OrderBy(c => c.BasePrice),
            items => items.OrderByDescending(c => c.BasePrice)),
        new SortOptionViewModel("Сортировать по рейтингу",
            items => items.OrderBy(c => c.Rating),
            items => items.OrderByDescending(c => c.Rating))
    };
    }

    public async Task LoadDataAsync()
    {
        await _enricher.EnrichAsync(_components);
        FilteredComponents = _components.ToObservableCollection();
        OnPropertyChanged(nameof(FilteredComponents));

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
