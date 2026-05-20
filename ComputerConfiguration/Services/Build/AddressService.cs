using ComputerConfiguration.DB;
using ComputerConfiguration.DTO;
using ComputerConfiguration.Models.Authentication;
using ComputerConfiguration.Models.UI;
using Microsoft.EntityFrameworkCore;

namespace ComputerConfiguration.Services.Build;

public class AddressService : IAddressService
{
    private readonly ComputerConfigurationDBContext _dbContext;
    public AddressService(ComputerConfigurationDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IResult> AddAddressAsync(AddressDTO addressData, int userId)
    {
        var user = await _dbContext.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
        if (user == null)
            return new Error("Address", "User", "Пользователь не авторизован");

        if (string.IsNullOrWhiteSpace(addressData.City))
            return new Error("Address", "City", "Город не указан");
        if (string.IsNullOrWhiteSpace(addressData.Street))
            return new Error("Address", "Street", "Улица не указана");
        if (string.IsNullOrWhiteSpace(addressData.HouseNumber))
            return new Error("Address", "HouseNumber", "Номер дома не указан");
        if (string.IsNullOrWhiteSpace(addressData.PostalCode))
            return new Error("Address", "PostalCode", "Почтовый индекс не указан");

        var address = new Address
        {
            Country = addressData.Country ?? "Россия",
            Region = addressData.Region,
            City = addressData.City,
            Street = addressData.Street,
            HouseNumber = addressData.HouseNumber,
            Building = addressData.Building,
            Apartment = addressData.Apartment,
            Entrance = addressData.Entrance,
            Floor = addressData.Floor,
            IntercomCode = addressData.IntercomCode,
            PostalCode = addressData.PostalCode
        };

        user.Addresses!.Add(address);
        await _dbContext.Addresses.AddAsync(address);
        await _dbContext.SaveChangesAsync();

        return new Success("Address", "Final", "Адрес успешно добавлен");
    }

    public async Task<IResult> EditAddressAsync(AddressDTO addressData, int addressId, int userId)
    {
        var user = await _dbContext.Users
                        .Include(u => u.Addresses)
                        .FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            return new Error("Address", "User", "Пользователь не найден");

        var address = await _dbContext.Addresses
                            .Include(a => a.Users)
                            .FirstOrDefaultAsync(a => a.Id == addressId);
        if (address == null)
            return new Error("Address", "Id", "Адрес не найден");

        if (address.Users!.Any(a => a.Id == userId) && !user.Addresses!.Any(a => a.Id == addressId))
            return new Error("Address", "User", "Этот адрес не принадлежит указанному пользователю");

        if (string.IsNullOrWhiteSpace(addressData.City))
            return new Error("Address", "City", "Город не указан");
        if (string.IsNullOrWhiteSpace(addressData.Street))
            return new Error("Address", "Street", "Улица не указана");
        if (string.IsNullOrWhiteSpace(addressData.HouseNumber))
            return new Error("Address", "HouseNumber", "Номер дома не указан");
        if (string.IsNullOrWhiteSpace(addressData.PostalCode))
            return new Error("Address", "PostalCode", "Почтовый индекс не указан");

        address.Country = addressData.Country ?? "Россия";
        address.Region = addressData.Region;
        address.City = addressData.City;
        address.Street = addressData.Street;
        address.HouseNumber = addressData.HouseNumber;
        address.Building = addressData.Building;
        address.Apartment = addressData.Apartment;
        address.Entrance = addressData.Entrance;
        address.Floor = addressData.Floor;
        address.IntercomCode = addressData.IntercomCode;
        address.PostalCode = addressData.PostalCode;

        await _dbContext.SaveChangesAsync();
        return new Success("Address", "Final", "Адрес успешно обновлён");
    }

    public async Task DeleteAddressAsync(Address address)
    {
        if (!await _dbContext.Addresses.AnyAsync(a => a == address))
            return;
        _dbContext.Addresses.Remove(address);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Address>> GetAddressAsync(int userId) => 
        await _dbContext.Addresses.Where(a => a.Users!.Any(u => u.Id == userId)).ToListAsync();
    public IEnumerable<Address> GetAddress(int userId) =>
        _dbContext.Addresses.Where(a => a.Users!.Any(u => u.Id == userId)).ToList();
}
