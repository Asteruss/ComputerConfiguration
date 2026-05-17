// FavoritesService.cs — принимает фабрику, создаёт контекст на каждую операцию
using ComputerConfiguration.DB;
using ComputerConfiguration.Models.Enums;
using ComputerConfiguration.Models.Favorites;
using ComputerConfiguration.Services.Favorites;
using Microsoft.EntityFrameworkCore;

public class FavoritesService : IFavoritesService
{
    private readonly Func<IFavoriteDBContext> _contextFactory;

    public FavoritesService(Func<IFavoriteDBContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<Favorite>> GetFavoritesAsync(int userId)
    {
        await using var ctx = _contextFactory();
        return await ctx.Favorites
            .AsNoTracking()
            .Where(f => f.UserId == userId)
            .ToListAsync();
    }

    public async Task<Favorite?> GetFavoriteAsync(int userId, int componentId, ComponentCategory type)
    {
        await using var ctx = _contextFactory();
        return await ctx.Favorites
            .Where(d => d.UserId == userId &&
                        d.ComponentId == componentId &&
                        d.ComponentCategory == type)
            .FirstOrDefaultAsync();
    }

    public async Task AddFavoriteAsync(int userId, int componentId, ComponentCategory type)
    {
        if (await IsFavoriteAsync(userId, componentId, type))
            return;

        await using var ctx = _contextFactory();
        var fav = new Favorite
        {
            ComponentId = componentId,
            UserId = userId,
            ComponentCategory = type
        };
        await ctx.Favorites.AddAsync(fav);
        await ctx.SaveChangesAsync_();
    }

    public async Task RemoveFavoriteAsync(int userId, int componentId, ComponentCategory type)
    {
        await using var ctx = _contextFactory();
        var favToDelete = await ctx.Favorites
            .Where(d => d.UserId == userId &&
                        d.ComponentId == componentId &&
                        d.ComponentCategory == type)
            .FirstOrDefaultAsync();

        if (favToDelete != null)
        {
            ctx.Favorites.Remove(favToDelete);
            await ctx.SaveChangesAsync_();
        }
    }

    public async Task<bool> IsFavoriteAsync(int userId, int componentId, ComponentCategory type)
    {
        await using var ctx = _contextFactory();
        return await ctx.Favorites
            .AnyAsync(f => f.UserId == userId &&
                           f.ComponentId == componentId &&
                           f.ComponentCategory == type);
    }

    public async Task ClearAllAsync(int userId)
    {
        await using var ctx = _contextFactory();
        await ctx.Favorites
            .Where(f => f.UserId == userId)
            .ExecuteDeleteAsync();
    }
}