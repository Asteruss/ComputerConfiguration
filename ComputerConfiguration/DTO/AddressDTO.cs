namespace ComputerConfiguration.DTO;

public class AddressDTO
{
    public string Country { get; set; }
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
}
