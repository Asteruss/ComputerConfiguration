using ComputerConfiguration.Models.Authentication;
using ComputerConfiguration.Models.Enums;
using ComputerConfiguration.Models.Orders;
using ComputerConfiguration.Models.Orders.Bonus;

namespace ComputerConfiguration.Services.Build;

public class BonusService
{
    // дo order
    public int GetBonuses(User user) => user.Balance;
    public int GetMaxBonusToEarn(PrivilegeLevel level, double price)
            => (int)price * level.PercentGet / 100;
    public int GetMaxBonusToSpend(User user, double price)
            => (int)Math.Min(price * user.PrivilegeLevel.PercentSpend / 100, GetBonuses(user));

    public double GetNewPrice(User user, double price) => price - GetMaxBonusToSpend(user, price);
    // после order
    public BonusHistory EarnBonuses(User user, Order order)
    {
        var bonus = GetMaxBonusToEarn(user.PrivilegeLevel, order.Price);
        user.Balance += bonus;
        return CreateBonusHistory(user, order, bonus, OperationType.Earn);
    }

    public BonusHistory SpendBonuses(User user, Order order)
    {
        var bonus = GetMaxBonusToSpend(user, order.Price);
        order.Price -= bonus;
        user.Balance -= bonus;
        return CreateBonusHistory(user, order, bonus, OperationType.Spent);
    }

    public PrivilegeLevel GetPrivilegeLevel(IEnumerable<PrivilegeLevel> levels, double sum) =>
        levels.Where(l => l.PriceThreshold <= sum).OrderByDescending(l => l.PriceThreshold).First();

    public void ChangePrivilegeLevel(User user, IEnumerable<PrivilegeLevel> levels, double sum)
    {
        var level = GetPrivilegeLevel(levels, sum);
        if (level.Id == user.PrivilegeLevel.Id) return;
        user.PrivilegeLevel = level;
    }

    public BonusHistory CreateBonusHistory(User user, Order order, int bonusCount, OperationType operation) =>
        new BonusHistory()
        {
            UserId = user.Id,
            OrderId = order.Id,
            BonusCount = bonusCount,
            Operation = operation
        };
}
