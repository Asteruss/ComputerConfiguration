using ComputerConfiguration.Models.Favorites;
using Microsoft.EntityFrameworkCore;
namespace ComputerConfiguration.DB;

public interface IFavoriteDBContext
{
    DbSet<Favorite> Favorites { get; set; }
    Task SaveChangesAsync_();
}
