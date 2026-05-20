using ComputerConfiguration.Models;
using ComputerConfiguration.Models.Authentication;
using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Components;
using ComputerConfiguration.Models.Favorites;
using ComputerConfiguration.Models.Orders;
using ComputerConfiguration.Models.Orders.Bonus;
using Microsoft.EntityFrameworkCore;

namespace ComputerConfiguration.DB;

public class ComputerConfigurationDBContext : DbContext, IFavoriteDBContext
{
    public DbSet<Cpu> Cpus { get; set; }
    public DbSet<Gpu> Gpus { get; set; }
    public DbSet<Case> Cases { get; set; }
    public DbSet<Motherboard> Motherboards { get; set; }
    public DbSet<Psu> Psus { get; set; }
    public DbSet<Ram> Rams { get; set; }
    public DbSet<Storage> Storages { get; set; }
    public DbSet<Cooler> Coolers { get; set; }
    public DbSet<ComputerBuild> ComputerBuilds { get; set; }
    public DbSet<AdditionalService> AdditionalServices { get; set; }
    public DbSet<AdditionalServiceOption> AdditionalServiceOptions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Order> Orders { get;set; }
    public DbSet<CoolerSocket> CoolerSockets { get; set; }
    public DbSet<BonusHistory> BonusHistory { get; set; }
    public DbSet<PrivilegeLevel> PrivilegeLevels { get; set; }
    public DbSet<Favorite> Favorites { get; set; }

    public ComputerConfigurationDBContext()
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ComputerConfigurationDB;Trusted_Connection=True;\r\n");
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<PrivilegeLevel>().HasData(
            new PrivilegeLevel() { Id=1, PercentGet=5, PercentSpend=5, PrivilegeName="Bronze", PriceThreshold=50000},
            new PrivilegeLevel() { Id=2, PercentGet=15, PercentSpend=10, PrivilegeName="Silver", PriceThreshold=100000}
            );
        modelBuilder.Entity<Role>().HasData(
            new Role() { Id=1, RoleName="User"}
            );

    }

    public Task SaveChangesAsync_() => SaveChangesAsync();
}
