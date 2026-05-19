using ComputerConfiguration.DB;
using ComputerConfiguration.Models.Enums;
using ComputerConfiguration.Models.Favorites;
using ComputerConfiguration.Services.Authentication;
using Microsoft.EntityFrameworkCore;

namespace ComputerConfiguration.Services.Favorites;

public class FavoriteFacade : IDisposable
{
    private readonly IFavoritesService _localService;
    private readonly IFavoritesService _dbService;
    private readonly IAuthService _authService;
    private readonly SemaphoreSlim _syncLock = new(1, 1);

    public FavoriteFacade(
       IDbContextFactory<ComputerConfigurationDBContext> dbFactory,
       IDbContextFactory<LocalDBContext> localFactory,
       IAuthService authService)
    {
        _localService = new FavoritesService(() => localFactory.CreateDbContext());
        _dbService = new FavoritesService(() => dbFactory.CreateDbContext());
        _authService = authService;
        _authService.UserChanged += OnUserChanged;
    }

    public void Dispose()
    {
        _authService.UserChanged -= OnUserChanged;
        _syncLock.Dispose();
    }
    private async void OnUserChanged()
    {
        if (!await _syncLock.WaitAsync(0)) 
            return;
        try
        {
            await Sync();
        }
        finally
        {
            _syncLock.Release();
        }
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
        if (!allFavLocal.Any())
            return;
        var id = _getUserId();
        foreach (var fav in allFavLocal)
            if (!await _dbService.IsFavoriteAsync(id, fav.ComponentId, fav.ComponentCategory))
                await _dbService.AddFavoriteAsync(id, fav.ComponentId, fav.ComponentCategory);

        await _localService.ClearAllAsync(0);
    }
}
