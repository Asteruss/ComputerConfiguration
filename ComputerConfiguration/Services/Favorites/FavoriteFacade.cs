using ComputerConfiguration.DB;
using ComputerConfiguration.Models.Enums;
using ComputerConfiguration.Models.Favorites;
using ComputerConfiguration.Services.Authentication;

namespace ComputerConfiguration.Services.Favorites;

public class FavoriteFacade : IDisposable
{
    private readonly FavoritesService _localService;
    private readonly FavoritesService _dbService;
    private readonly IAuthService _authService;

    public FavoriteFacade(ComputerConfigurationDBContext dbContext, LocalDBContext local,
        IAuthService authService)
    {
        _localService = new FavoritesService(local);
        _dbService = new FavoritesService(dbContext);
        _authService = authService;
        _authService.UserChanged += OnUserChanged;
    }

    public void Dispose()
    {
        _authService.UserChanged -= OnUserChanged;
    }

    private async void OnUserChanged()
    {
        await Sync();
    }
    private int _getUserId() => _authService.CurrentUser?.Id ?? 0;
    private IFavoritesService _chooseService() => (_authService.IsAuthenticated) ? _dbService : _localService;

    public async Task<IEnumerable<Favorite>> GetFavoritesAsync() =>
        await _chooseService().GetFavoritesAsync(_getUserId());
    public async Task<Favorite?> GetFavoriteAsync(int componentId, ComponentCategory type) =>
        await _chooseService().GetFavoriteAsync(_getUserId(), componentId, type);
    public async Task AddFavoriteAsync(int componentId, ComponentCategory type) =>
        await _chooseService().AddFavoriteAsync(_getUserId(), componentId, type);

    public async Task RemoveFavoriteAsync(int componentId, ComponentCategory type) =>
        await _chooseService().RemoveFavoriteAsync(_getUserId(), componentId, type);

    public async Task<bool> IsFavoriteAsync(int componentId, ComponentCategory type) =>
        await _chooseService().IsFavoriteAsync(_getUserId(), componentId, type);

    public async Task Sync()
    {
        if (!_authService.IsAuthenticated)
            return;

        var allFavLocal = await _localService.GetFavoritesAsync(0);
        var id = _getUserId();
        foreach (var fav in allFavLocal)
            if (!await _dbService.IsFavoriteAsync(id, fav.ComponentId, fav.ComponentCategory))
                await _dbService.AddFavoriteAsync(id, fav.ComponentId, fav.ComponentCategory);

        await _localService.ClearAllAsync(0);
    }
}
