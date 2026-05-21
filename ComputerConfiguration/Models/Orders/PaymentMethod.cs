namespace ComputerConfiguration.Models.Orders;
public class PaymentMethod
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsCash { get; set; }
    public bool IsOnline { get; set; }
}
