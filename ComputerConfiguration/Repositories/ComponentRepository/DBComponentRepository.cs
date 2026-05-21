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
    public IEnumerable<Cpu> GetCpus() => [.. _db.Cpus];
    public IEnumerable<Gpu> GetGpus() => [.. _db.Gpus];
    public IEnumerable<Motherboard> GetMotherboards() => [.. _db.Motherboards];
    public IEnumerable<Ram> GetRam() => [.. _db.Rams];
    public IEnumerable<Psu> GetPsu() => [.. _db.Psus];
    public IEnumerable<Cooler> GetCoolers() => [.. _db.Coolers.Include(c => c.SocketSupport)];
    public IEnumerable<Case> GetCases() => [.. _db.Cases];
    public IEnumerable<Storage> GetStorages() => [.. _db.Storages];

}
