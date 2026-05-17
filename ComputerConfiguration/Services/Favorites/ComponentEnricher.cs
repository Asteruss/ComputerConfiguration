using ComputerConfiguration.Models;

namespace ComputerConfiguration.Services.Favorites;

public class ComponentEnricher : IComponentEnricher
{
    private readonly FavoriteFacade _favoriteFacade;
    public ComponentEnricher(FavoriteFacade favoriteFacade)
    {
        _favoriteFacade = favoriteFacade;
    }
    public async Task EnrichAsync<T>(IEnumerable<T> components) where T : ComponentBase
    {
        if (components == null || !components.Any()) return;
        var favorites = await _favoriteFacade.GetFavoritesAsync();
        foreach (var comp in components)
            comp.IsFavorite = favorites.Any(f => f.ComponentId == comp.Id && f.ComponentCategory == comp.ComponentCategory);
        
    }
}
