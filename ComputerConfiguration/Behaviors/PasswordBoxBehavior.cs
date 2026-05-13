using Microsoft.Xaml.Behaviors;
using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace ComputerConfiguration.Behaviors;

public class PasswordBoxBehavior : Behavior<PasswordBox>
{
    public static readonly DependencyProperty SecurePasswordProperty =
        DependencyProperty.RegisterAttached("SecurePassword", typeof(SecureString), typeof(PasswordBoxBehavior), new FrameworkPropertyMetadata(null));

    public SecureString SecurePassword
    {
        get => (SecureString)GetValue(SecurePasswordProperty);
        set => SetValue(SecurePasswordProperty, value);
    }

    protected override void OnAttached()
    {
        AssociatedObject.PasswordChanged += OnPasswordChanged;
        base.OnAttached();
    }

    private void OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        SecurePassword = AssociatedObject.SecurePassword;
    }
}
