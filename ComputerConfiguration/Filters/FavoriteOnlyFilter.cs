using ComputerConfiguration.Models;
namespace ComputerConfiguration.Filters;

public class FavoriteOnlyFilter : FilterBase
{
    private bool _isEnabled;
    public bool IsEnabled
    {
        get => _isEnabled;
        set { _isEnabled = value; OnPropertyChanged(); }
    }

    public override bool Matches(ComponentBase component)
    {
        if (!IsEnabled) return true;
        return component.IsFavorite;
    }

}
