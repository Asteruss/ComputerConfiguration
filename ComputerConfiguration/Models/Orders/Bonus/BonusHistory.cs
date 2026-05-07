using ComputerConfiguration.Models.Authentication;
using ComputerConfiguration.Models.Enums;

namespace ComputerConfiguration.Models.Orders.Bonus;

public class BonusHistory
{
    public int Id { get; set; }
    public int BonusCount { get; set; }
    public OperationType Operation { get; set; }
    public int? OrderId { get; set; }
    public int? UserId { get; set; }
    public Order Order { get; set; }
    public User User { get; set; }
}
