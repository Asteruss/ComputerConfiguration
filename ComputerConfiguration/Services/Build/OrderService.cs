using ComputerConfiguration.DB;
using ComputerConfiguration.Models.Orders;
namespace ComputerConfiguration.Services.Build;

public class OrderService
{
    private readonly ComputerConfigurationDBContext _dbContext;
    public Order CreateOrder(int userId, int buildId, double price, int addressId)
    {
        var order = new Order();
        order.UserId = userId;
        order.ComputerBuildId = buildId;
        order.Price = price;
        order.CreationDate = DateTime.Now;
        order.AddressId = addressId;
        return order;
    }

    public double GetOrderCostsBy(int userId, int period = 720) => _dbContext.Orders.Where(o => o.UserId == userId).Sum(o => o.Price);
}
