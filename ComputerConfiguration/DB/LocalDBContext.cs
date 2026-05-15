using ComputerConfiguration.Models.Favorites;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ComputerConfiguration.DB;

public class LocalDBContext : DbContext, IFavoriteDBContext
{
    public DbSet<Favorite> Favorites { get; set; }
    public async Task SaveChangesAsync_() => await SaveChangesAsync();
    public LocalDBContext()
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=favorite.db");
        base.OnConfiguring(optionsBuilder);
    }
}
