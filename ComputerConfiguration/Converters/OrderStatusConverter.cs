using ComputerConfiguration.Models.Enums;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ComputerConfiguration.Converters;

public class OrderStatusConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is OrderStatus status)
        {
            return status switch
            {
                OrderStatus.WaitForPayment => "Ожидает оплаты",
                OrderStatus.Accepted => "Принят",
                OrderStatus.InProgress => "В обработке",
                OrderStatus.InDelivery => "Доставляется",
                OrderStatus.Delivered => "Доставлен",
                OrderStatus.Done => "Завершён",
                _ => status.ToString()
            };
        }
        return "";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
