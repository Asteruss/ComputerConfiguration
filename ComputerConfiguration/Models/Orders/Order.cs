using ComputerConfiguration.Models.Authentication;
using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Enums;
using ComputerConfiguration.Models.Orders.Bonus;

namespace ComputerConfiguration.Models.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public double Price { get; set; }
        public int? ComputerBuildId { get; set; }
        public int? UserId { get; set; }
        public int? AddressId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public ComputerBuild? ComputerBuild { get; set; }
        public User? User { get; set; }
        public Address? Address { get; set; }
        public List<BonusHistory>? BonusHistories { get; set; }

    }
}
