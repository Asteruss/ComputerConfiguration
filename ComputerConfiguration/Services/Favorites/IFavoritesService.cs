using ComputerConfiguration.Models.Enums;
using ComputerConfiguration.Models.Favorites;

namespace ComputerConfiguration.Services.Favorites;

public interface IFavoritesService
{
    Task<IEnumerable<Favorite>> GetFavoritesAsync(int userId);
    Task<Favorite?> GetFavoriteAsync(int userId, int componentId, ComponentCategory type);
    Task AddFavoriteAsync(int userId, int componentId, ComponentCategory type);
    Task RemoveFavoriteAsync(int userId, int componentId, ComponentCategory type);
    Task<bool> IsFavoriteAsync(int userId, int componentId, ComponentCategory type);
    Task ClearAllAsync(int userId);
}
