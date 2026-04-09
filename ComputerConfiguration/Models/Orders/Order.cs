using ComputerConfiguration.Models.Authentication;
using ComputerConfiguration.Models.Build;

namespace ComputerConfiguration.Models.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public int? CompuiterBuildId { get; set; }
        public int? UserId { get; set; }
        public int? AddressId { get; set; }
        public ComputerBuild? ComputerBuild { get; set; }
        public User? User { get; set; }
        public Address? Address { get; set; }
    }
}
