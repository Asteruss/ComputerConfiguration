using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace ComputerConfiguration.Styles.Controls;

public class IconTextToggleButton : ToggleButton
{
    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
        nameof(Icon), typeof(ImageSource), typeof(IconTextToggleButton)
        );

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text), typeof(string), typeof(IconTextToggleButton)
        );

    public ImageSource Icon
    {
        get => (ImageSource)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}