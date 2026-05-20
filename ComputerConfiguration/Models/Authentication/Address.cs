using ComputerConfiguration.Models.Orders;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerConfiguration.Models.Authentication;

public class Address
{
    public int Id { get; set; }
    public string Country { get; set; } = "Россия";
    public string Region { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public string? Building { get; set; }
    public string? Apartment { get; set; }
    public string? Entrance { get; set; }
    public int? Floor { get; set; }
    public string? IntercomCode { get; set; }
    public string PostalCode { get; set; }
    public List<User>? Users { get; set; }
    public Order Order { get; set; }

    [NotMapped]
    public string FullAddress
    {
        get
        {
            var parts = new List<string>();

            if (!string.IsNullOrWhiteSpace(Country)) parts.Add(Country);
            if (!string.IsNullOrWhiteSpace(Region)) parts.Add(Region);
            if (!string.IsNullOrWhiteSpace(City)) parts.Add(City);
            if (!string.IsNullOrWhiteSpace(Street)) parts.Add(Street);
            if (!string.IsNullOrWhiteSpace(HouseNumber)) parts.Add(HouseNumber);
            if (!string.IsNullOrWhiteSpace(Building)) parts.Add($"корп.{Building}");
            if (!string.IsNullOrWhiteSpace(Apartment)) parts.Add($"кв.{Apartment}");
            if (!string.IsNullOrWhiteSpace(Entrance)) parts.Add($"под.{Entrance}");
            if (Floor.HasValue) parts.Add($"эт.{Floor}");
            if (!string.IsNullOrWhiteSpace(IntercomCode)) parts.Add($"домофон {IntercomCode}");
            if (!string.IsNullOrWhiteSpace(PostalCode)) parts.Add(PostalCode);

            return string.Join(", ", parts);
        }
    }
}