using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ComputerConfiguration.Behaviors;

public static class ListBoxBehavior
{
    public static readonly DependencyProperty SelectedItemsProperty =
        DependencyProperty.RegisterAttached("SelectedItems", typeof(IList), typeof(ListBoxBehavior),
            new FrameworkPropertyMetadata(null, OnSelectedItemsChanged));

    public static void SetSelectedItems(DependencyObject element, IList value) =>
        element.SetValue(SelectedItemsProperty, value);

    public static IList GetSelectedItems(DependencyObject element) =>
        (IList)element.GetValue(SelectedItemsProperty);

    private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ListBox listBox)
            listBox.SelectionChanged += (sender, args) =>
            {
                var items = GetSelectedItems(listBox);
                items?.Clear();
                foreach (var item in listBox.SelectedItems)
                    items?.Add(item);
            };

    }
}
