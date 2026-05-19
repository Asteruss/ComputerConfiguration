using ComputerConfiguration.Builder;
using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Enums;

namespace ComputerConfiguration.ViewModels;

public class ServiceSelectionViewModel : ViewModelBase
{
    private readonly IComputerBuildSession _computerSession;
    public AdditionalService Service { get; }
    public IEnumerable<AdditionalServiceOption> Options { get; }

    private bool _isSelected;
    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (_isSelected != value)
            {
                _isSelected = value;
                OnPropertyChanged();
                UpdateSelection();
            }
        }
    }

    private AdditionalServiceOption _selectedOption;
    public AdditionalServiceOption SelectedOption
    {
        get => _selectedOption;
        set
        {
            if (_selectedOption != value)
            {
                _selectedOption = value;
                OnPropertyChanged();
                UpdateSelection();
            }
        }
    }
    private void UpdateSelection()
    {
        _computerSession.RemoveAdditionalOptions(Service);

        if (IsSelected && SelectedOption != null)
            _computerSession.AddAdditionalOption(SelectedOption);
    }

    public ServiceSelectionViewModel(AdditionalService service, IComputerBuildSession computerSession)
    {
        Service = service;
        Options = service.AdditionalServiceOptions ?? [];
        _computerSession = computerSession;
        if (Service.OptionType == OptionType.SingleOption && Options.Any())
            _selectedOption = Options.First();
    }
}
