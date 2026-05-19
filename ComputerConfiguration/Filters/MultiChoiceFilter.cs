using ComputerConfiguration.Filters.Strategies;
using ComputerConfiguration.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Filters;

public class MultiChoiceFilter : FilterBase
{
    public ObservableCollection<object> Options { get; set; } = new();
    private ObservableCollection<object> _selectedItems;
    public ObservableCollection<object> SelectedItems
    {
        get => _selectedItems;
        set
        {
            if (_selectedItems != null)
                _selectedItems.CollectionChanged -= OnSelectedItemsChanged;

            _selectedItems = value;

            if (_selectedItems != null)
                _selectedItems.CollectionChanged += OnSelectedItemsChanged;

            OnPropertyChanged();
            OnPropertyChanged(nameof(Value));

        }
    }
    private void OnSelectedItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(SelectedItems));
        OnPropertyChanged(nameof(Value));

    }
    public MultiChoiceFilter()
    {
        SelectedItems = [];
    }

    public override bool Matches(ComponentBase component)
    {
        if (SelectedItems == null || SelectedItems.Count == 0)
            return true;

        var propValue = component.GetType().GetProperty(Name)?.GetValue(component);
        if (propValue == null) return false;

        return SelectedItems.Any(filterValue => MatchStrategy.IsMatch(propValue, filterValue));
    }
}