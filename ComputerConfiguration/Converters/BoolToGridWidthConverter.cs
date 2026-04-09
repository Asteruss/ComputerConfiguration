using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ComputerConfiguration.Converters
{
    [ValueConversion(typeof(bool), typeof(GridLength))]
    public class BoolToGridWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            (bool)value == true ? new GridLength(0.2, GridUnitType.Star) : new GridLength(0);


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => true;

    }
}
