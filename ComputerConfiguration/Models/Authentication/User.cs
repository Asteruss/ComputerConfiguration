using ComputerConfiguration.Models.Orders;
using ComputerConfiguration.Models.Orders.Bonus;

namespace ComputerConfiguration.Models.Authentication;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Sername { get; set; }
    public string PasswordHash { get; set; }
    public DateTime RegistrationDate { get; set; }
    public int? RoleId { get; set; }
    public int Balance { get; set; } = 0;
    public int PrivilegeLevelId { get; set; }
    public PrivilegeLevel? PrivilegeLevel { get; set; }
    public List<BonusHistory>? BonusHistories { get; set; }
    public Role? Role { get; set; }
    public List<Address>? Addresses { get; set; }
    public List<Order>? Orders { get; set; }


}
