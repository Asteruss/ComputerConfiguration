using ComputerConfiguration.DB;
using ComputerConfiguration.Models.Components;
using Microsoft.EntityFrameworkCore;

namespace ComputerConfiguration.Repositories.ComponentRepository;

public class DBComponentRepository : IComponentRepository
{
    private readonly ComputerConfigurationDBContext _db;
    public DBComponentRepository(ComputerConfigurationDBContext dbContext)
    {
        _db = dbContext;
    }
    public IEnumerable<Cpu> GetCpus() => [.. _db.Cpus.AsNoTracking()];
    public IEnumerable<Gpu> GetGpus() => [.. _db.Gpus.AsNoTracking()];
    public IEnumerable<Motherboard> GetMotherboards() => [.. _db.Motherboards.AsNoTracking()];
    public IEnumerable<Ram> GetRam() => [.. _db.Rams.AsNoTracking()];
    public IEnumerable<Psu> GetPsu() => [.. _db.Psus.AsNoTracking()];
    public IEnumerable<Cooler> GetCoolers() => [.. _db.Coolers.Include(c => c.SocketSupport).AsNoTracking()];
    public IEnumerable<Case> GetCases() => [.. _db.Cases.AsNoTracking()];
    public IEnumerable<Storage> GetStorages() => [.. _db.Storages.AsNoTracking()];

}
