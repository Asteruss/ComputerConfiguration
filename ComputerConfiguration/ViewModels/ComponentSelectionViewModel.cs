using ComputerConfiguration.Builder;
using ComputerConfiguration.Commands;
using ComputerConfiguration.Converters;
using ComputerConfiguration.Filters;
using ComputerConfiguration.Models;
using ComputerConfiguration.Models.Components;
using ComputerConfiguration.Models.Enums;
using ComputerConfiguration.Services.Authentication;
using ComputerConfiguration.Services.Components;
using ComputerConfiguration.Services.Favorites;
using System.Collections.ObjectModel;

namespace ComputerConfiguration.ViewModels;

public class ComponentSelectionViewModel : ViewModelBase
{
    private List<ComponentBase> _components { get; set; }
    public ObservableCollection<ComponentBase> Components { get; set; }
    public ObservableCollection<FilterBase> Filters { get; } = new();
    public ObservableCollection<SortOptionViewModel> SortOptions { get; set; }
    private readonly ComponentCategory _category;
    private readonly IComputerBuildSession _session;
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
                Components = opt.Sort()(Components).ToObservableCollection();
                OnPropertyChanged(nameof(Components));
            }
        }));
    }


    private RelayCommand _selectCommand;
    public RelayCommand SelectCommand
    {
        get => _selectCommand ??= new((component) =>
        {
            if (component is Cpu cpu)
                _session.SetCpu(cpu);
            if (component is Gpu gpu)
                _session.SetGpu(gpu);
            if (component is Motherboard mot)
                _session.SetMotherboard(mot);
            if (component is Ram ram)
                _session.AddRam(ram);
            if (component is Cooler cooler)
                _session.SetCooler(cooler);
            if (component is Psu psu)
                _session.SetPsu(psu);
            if (component is Case case_)
                _session.SetCase(case_);
            if (component is Storage storage)
                _session.AddStorage(storage);
        });
    }

    private RelayCommand _selectFakeCommand;
    public RelayCommand SelectFakeCommand
    {
        get => _selectFakeCommand ??= new((component) =>
        {
            if (component is Cpu cpu)
                _session.SetCpu(cpu, ComponentStatus.SelectedAsFake);
            if (component is Gpu gpu)
                _session.SetGpu(gpu, ComponentStatus.SelectedAsFake);
            if (component is Motherboard mot)
                _session.SetMotherboard(mot, ComponentStatus.SelectedAsFake);
            if (component is Ram ram)
                _session.AddRam(ram, ComponentStatus.SelectedManyAsFake);
            if (component is Cooler cooler)
                _session.SetCooler(cooler, ComponentStatus.SelectedAsFake);
            if (component is Psu psu)
                _session.SetPsu(psu, ComponentStatus.SelectedAsFake);
            if (component is Case case_)
                _session.SetCase(case_, ComponentStatus.SelectedAsFake);
            if (component is Storage storage)
                _session.AddStorage(storage, ComponentStatus.SelectedManyAsFake);

        });
    }
    private RelayCommand _removeCommand;
    public RelayCommand RemoveCommand
    {
        get => _removeCommand ??= new((component) =>
        {
            if (component is Cpu cpu)
                _session.RemoveCpu();
            if (component is Gpu gpu)
                _session.RemoveGpu();
            if (component is Motherboard mot)
                _session.RemoveMotherboard();
            if (component is Ram ram)
                _session.ClearRams(ram);
            if (component is Cooler cooler)
                _session.RemoveCooler();
            if (component is Psu psu)
                _session.RemovePsu();
            if (component is Case case_)
                _session.RemoveCase();
            if (component is Storage storage)
                _session.ClearStorages(storage);
        });
    }
    private RelayCommand _removeFakeCommand;
    public RelayCommand RemoveFakeCommand
    {
        get => _removeFakeCommand ??= new((component) =>
        {
            if (component is Ram ram)
                _session.ClearRams(ram, ComponentStatus.SelectedManyAsFake);
            if (component is Storage storage)
                _session.ClearStorages(storage, ComponentStatus.SelectedManyAsFake);
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

    public ComponentSelectionViewModel(IComponentFactory factory, 
        IComponentFilterProvider filterProvider,
        ComponentCategory category, 
        IComputerBuildSession session, 
        FavoriteFacade favoriteFacade,
        IComponentEnricher enricher,
        IAuthService authService)
    {
        _category = category;
        _favoriteFacade = favoriteFacade;
        _enricher = enricher;

        _components = [.. factory.GetComponent(category)];

        _ = LoadDataAsync();
        authService.UserChanged += () => _ = LoadDataAsync();
        Filters = filterProvider.GetFilters(category, _components).ToObservableCollection();
        OnPropertyChanged(nameof(Filters));


        foreach (var filter in Filters)
            filter.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(FilterBase.Value))
                    _applyFilters();
            };
        _session = session;

        SortOptions = new ObservableCollection<SortOptionViewModel>
        {
            new("Сортировать по названию",
                items => items.OrderBy(c => c.Name),
                items => items.OrderByDescending(c => c.Name)),
            new("Сортировать по цене",
                items => items.OrderBy(c => c.BasePrice),
                items => items.OrderByDescending(c => c.BasePrice)),
            new("Сортировать по рейтингу",
                items => items.OrderBy(c => c.Rating),
                items => items.OrderByDescending(c => c.Rating))
        };
    }

    public async Task LoadDataAsync()
    {
        await _enricher.EnrichAsync(_components);
        Components = _components.ToObservableCollection();
        OnPropertyChanged(nameof(Components));

    }

    private void _applyFilters()
    {
        var filtered = _components;
        foreach (var filter in Filters)
            filtered = [.. filtered.Where(c => filter.Matches(c))];
        Components = filtered.ToObservableCollection();
        OnPropertyChanged(nameof(Components));

    }
}
