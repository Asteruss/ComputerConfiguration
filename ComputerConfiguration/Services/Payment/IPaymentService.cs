using ComputerConfiguration.Models.Orders;

namespace ComputerConfiguration.Services.Payment;

public interface IPaymentService
{
    List<PaymentMethod> GetPaymentMethods();
    Task<bool> ProcessPaymentAsync(int orderId, int paymentMethodId, double price);
}
