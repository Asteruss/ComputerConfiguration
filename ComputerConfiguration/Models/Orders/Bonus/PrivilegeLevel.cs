using ComputerConfiguration.Models.Authentication;

namespace ComputerConfiguration.Models.Orders.Bonus;

public class PrivilegeLevel
{
    public int Id { get; set; }
    public string PrivilegeName { get; set; }
    public int PercentGet { get; set; }
    public int PercentSpend { get; set; }
    public int PriceThreshold { get; set; }
    public List<User> Users { get; set; }
}
