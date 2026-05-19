using ComputerConfiguration.Models;
namespace ComputerConfiguration.Filters;

public class BoolFilter : FilterBase
{
    private bool _isEnabled;
    private bool _targetValue;
    private readonly Func<ComponentBase, bool> _propertyGetter;

    public bool IsEnabled
    {
        get => _isEnabled;
        set
        {
            if (_isEnabled != value)
            {
                _isEnabled = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Value));
            }
        }
    }

    public bool TargetValue
    {
        get => _targetValue;
        set
        {
            if (_targetValue != value)
            {
                _targetValue = value;
                OnPropertyChanged();
            }
        }
    }

    public BoolFilter(Func<ComponentBase, bool> propertyGetter, string displayName = null, bool targetValue = true)
    {
        _propertyGetter = propertyGetter;
        TargetValue = targetValue;
        if (!string.IsNullOrEmpty(displayName))
            DisplayName = displayName;
    }

    public override bool Matches(ComponentBase component)
    {
        if (!IsEnabled) return true;
        return _propertyGetter(component) == TargetValue;
    }
}