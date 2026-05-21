using ComputerConfiguration.Models.Orders;
namespace ComputerConfiguration.Services.Payment;

public class FakePaymentService : IPaymentService
{
    public List<PaymentMethod> GetPaymentMethods() => new List<PaymentMethod>
    {
        new PaymentMethod { Id = 1, Name = "Банковская карта", Description = "Visa, MasterCard, МИР", IsOnline = true },
        new PaymentMethod { Id = 2, Name = "Наличные при получении", Description = "Оплата курьеру", IsCash = true },
        new PaymentMethod { Id = 3, Name = "Яндекс pay", Description = "Онлайн-кошелёк", IsOnline = true },
        new PaymentMethod { Id = 4, Name = "СБП", Description = "Система быстрых платежей", IsOnline = true }
    };
    
    
    public Task<bool> ProcessPaymentAsync(int orderId, int paymentMethodId, double price)
    {
        return Task.FromResult(true);
    }
}
