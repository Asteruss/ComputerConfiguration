using ComputerConfiguration.DB;
using ComputerConfiguration.Models.Enums;
using ComputerConfiguration.Models.Favorites;
using Microsoft.EntityFrameworkCore;

namespace ComputerConfiguration.Services.Favorites;

public class FavoritesService : IFavoritesService
{
    private IFavoriteDBContext _dbContext;
    public FavoritesService(IFavoriteDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Favorite>> GetFavoritesAsync(int userId) =>
        await _dbContext.Favorites.Where(f => f.UserId == userId).AsNoTracking().ToListAsync();
    public async Task<Favorite?> GetFavoriteAsync(int userId, int componentId, ComponentCategory type) =>
        await _dbContext.Favorites.Where(d => d.UserId == userId &&
                                              d.ComponentId == componentId &&
                                              d.ComponentCategory == type).FirstOrDefaultAsync();


    public async Task AddFavoriteAsync(int userId, int componentId, ComponentCategory type)
    {
        if (await IsFavoriteAsync(userId, componentId, type))
            return;

        var fav = new Favorite()
        {
            ComponentId = componentId,
            UserId = userId,
            ComponentCategory = type
        };
        
        await _dbContext.Favorites.AddAsync(fav);
        await _dbContext.SaveChangesAsync_();
    }

    public async Task RemoveFavoriteAsync(int userId, int componentId, ComponentCategory type)
    {
        var favToDelete = await GetFavoriteAsync(userId, componentId, type);
        if (favToDelete != null)
        {
            _dbContext.Favorites.Remove(favToDelete);
            await _dbContext.SaveChangesAsync_();
        }
            
    }

    public async Task<bool> IsFavoriteAsync(int userId, int componentId, ComponentCategory type) =>
        await _dbContext.Favorites.AnyAsync(f => f.UserId == userId && f.ComponentId == componentId && f.ComponentCategory == type);

    public async Task ClearAllAsync(int userId)
    {
        await _dbContext.Favorites
               .Where(f => f.UserId == userId)
               .ExecuteDeleteAsync();
        await _dbContext.SaveChangesAsync_();
    }

}
