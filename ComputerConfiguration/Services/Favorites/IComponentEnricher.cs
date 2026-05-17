using ComputerConfiguration.Models;

namespace ComputerConfiguration.Services.Favorites;

public interface IComponentEnricher
{
    Task EnrichAsync<T>(IEnumerable<T> components) where T : ComponentBase;
}
