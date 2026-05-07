using ComputerConfiguration.Models.Authentication;
using ComputerConfiguration.Models.Enums;
using ComputerConfiguration.Models.Orders;
using ComputerConfiguration.Models.Orders.Bonus;

namespace ComputerConfiguration.Services.Build;

public class BonusService
{
    // дo order
    public int GetBonuses(User user) => user.Balance;
    public int GetMaxBonusToEarn(PrivilegeLevel level, int price)
            => price * level.PercentGet / 100;
    public int GetMaxBonusToSpend(User user, int price)
            => Math.Min(price * user.PrivilegeLevel.PercentSpend / 100, user.Balance);

    public int GetNewPrice(User user, int price) => price - GetMaxBonusToSpend(user, price);
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

    public PrivilegeLevel GetPrivilegeLevel(IEnumerable<PrivilegeLevel> levels, int sum) =>
        levels.Where(l => l.PriceThreshold <= sum).OrderByDescending(l => l.PriceThreshold).First();

    public void ChangePrivilegeLevel(User user, IEnumerable<PrivilegeLevel> levels, int sum)
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
