using ComputerConfiguration.DTO;
using ComputerConfiguration.Models.Authentication;
using ComputerConfiguration.Models.UI;

namespace ComputerConfiguration.Services.Build;

public interface IAddressService
{
    public Task<IResult> AddAddressAsync(AddressDTO addressData, int userId);
    public Task<IResult> EditAddressAsync(AddressDTO addressData, int adressId, int userId);
    public Task DeleteAddressAsync(Address address);
    public Task<IEnumerable<Address>> GetAddressAsync(int userId);
    public IEnumerable<Address> GetAddress(int userId);
}
