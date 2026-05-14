using System.Globalization;
using System.Windows.Data;

namespace ComputerConfiguration.Converters;

public class SignConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool useBonuses)
        {
            return useBonuses ? "-" : "+";
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
