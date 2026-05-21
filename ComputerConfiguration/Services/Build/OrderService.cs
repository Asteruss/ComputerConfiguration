using ComputerConfiguration.DB;
using ComputerConfiguration.Models.Orders;
using ComputerConfiguration.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace ComputerConfiguration.Services.Build;

public class OrderService
{
    private readonly ComputerConfigurationDBContext _dbContext;
    public OrderService(ComputerConfigurationDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Order CreateOrder(int userId, int buildId, double price, int addressId)
    {
        var order = new Order();
        order.UserId = userId;
        order.ComputerBuildId = buildId;
        order.Price = price;
        order.CreationDate = DateTime.Now;
        order.AddressId = addressId;
        order.OrderStatus = OrderStatus.WaitForPayment;
        return order;
    }

    public double GetOrderCostsBy(int userId, int period = 720) => 
        _dbContext.Orders.Where(o => o.UserId == userId).Sum(o => o.Price);

    public async Task<Order> GetOrderAsync(int orderId) => 
        await _dbContext.Orders.FindAsync(orderId);

    public async Task<Order> GetOrderAsync(int orderId, OrderStatus status) =>
        await _dbContext.Orders.Include(o => o.ComputerBuild).Where(o => o.Id == orderId && o.OrderStatus == status).FirstOrDefaultAsync();

}
