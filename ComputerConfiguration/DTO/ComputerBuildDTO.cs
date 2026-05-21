using ComputerConfiguration.Commands;
using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Components;
using System.Collections.ObjectModel;

namespace ComputerConfiguration.DTO;

public class ComputerBuildDTO : NotifyPropertyChanged
{
    private Cpu _cpu;
    public Cpu Cpu
    {
        get => _cpu; set
        {
            _cpu = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsSelectedCpu));
            OnPropertyChanged(nameof(IsSelectedAnything));
        }
    }

    private Gpu _gpu;
    public Gpu Gpu
    {
        get => _gpu; set
        {
            _gpu = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsSelectedGpu));
            OnPropertyChanged(nameof(IsSelectedAnything));
        }
    }

    private Motherboard _motherboard;
    public Motherboard Motherboard
    {
        get => _motherboard; set
        {
            _motherboard = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsSelectedMotherboard));
            OnPropertyChanged(nameof(IsSelectedAnything));
        }
    }

    private ObservableCollection<BuildRam> _rams = new();
    public ObservableCollection<BuildRam> Rams
    {
        get => _rams; set
        {
            _rams = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsSelectedRam));
            OnPropertyChanged(nameof(IsSelectedAnything));
        }
    }

    private Case _case;
    public Case Case
    {
        get => _case; set
        {
            _case = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsSelectedCase));
            OnPropertyChanged(nameof(IsSelectedAnything));
        }
    }

    private Cooler _cooler;
    public Cooler Cooler
    {
        get => _cooler; set
        {
            _cooler = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsSelectedCooler));
            OnPropertyChanged(nameof(IsSelectedAnything));
        }
    }

    private Psu _psu;
    public Psu Psu
    {
        get => _psu; set
        {
            _psu = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsSelectedPsu));
            OnPropertyChanged(nameof(IsSelectedAnything));
        }
    }

    private ObservableCollection<BuildStorage> _storages = new();
    public ObservableCollection<BuildStorage> Storages
    {
        get => _storages; set
        {
            _storages = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsSelectedStorage));
            OnPropertyChanged(nameof(IsSelectedAnything));
        }
    }

    private List<AdditionalServiceOption> _selectedAdditionalServices = new();
    public List<AdditionalServiceOption> SelectedAdditionalServices
    {
        get => _selectedAdditionalServices; set
        {
            _selectedAdditionalServices = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsSelectedAdditionalService));
        }
    }

    // Флаги наличия компонентов
    public bool IsSelectedCpu => _cpu != null;
    public bool IsSelectedGpu => _gpu != null;
    public bool IsSelectedMotherboard => _motherboard != null;
    public bool IsSelectedRam => _rams.Count != 0;
    public bool IsSelectedCase => _case != null;
    public bool IsSelectedCooler => _cooler != null;
    public bool IsSelectedPsu => _psu != null;
    public bool IsSelectedStorage => _storages.Count != 0;
    public bool IsSelectedAdditionalService => _selectedAdditionalServices.Count != 0;

    // Общий флаг
    public bool IsSelectedAnything => IsSelectedCpu || IsSelectedGpu || IsSelectedMotherboard || IsSelectedRam ||
                                      IsSelectedCase || IsSelectedCooler || IsSelectedPsu || IsSelectedStorage;
    public ComputerBuildDTO()
    {
        Rams.CollectionChanged += (s, e) => { OnPropertyChanged(nameof(IsSelectedRam)); OnPropertyChanged(nameof(IsSelectedAnything)); };
        Storages.CollectionChanged += (s, e) => { OnPropertyChanged(nameof(IsSelectedStorage)); OnPropertyChanged(nameof(IsSelectedAnything)); };
    }
}
