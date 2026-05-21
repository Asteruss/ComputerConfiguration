namespace ComputerConfiguration.Models.UI;

public class OrderSuccess : Success
{
    public int OrderId { get; set; }
    public double OrderPrice { get; set; }
    public OrderSuccess(string source, string block, string message, int orderId, double orderPrice) : base(source, block, message)
    {
        OrderId = orderId;
        OrderPrice = orderPrice;
    }
}
