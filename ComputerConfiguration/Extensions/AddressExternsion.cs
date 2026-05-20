using ComputerConfiguration.DTO;
using ComputerConfiguration.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Extensions;

public static class AddressExternsion
{
    public static AddressDTO ToDto(this Address address) =>
        new()
        {
            Country = address.Country,
            Region = address.Region,
            City = address.City,
            Street = address.Street,
            HouseNumber = address.HouseNumber,
            Building = address.Building,
            Apartment = address.Apartment,
            Entrance = address.Entrance,
            Floor = address.Floor,
            IntercomCode = address.IntercomCode,
            PostalCode = address.PostalCode
        };
    
}
