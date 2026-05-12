namespace ComputerConfiguration.Services.Build.Compability;

public class CompatibilityRuleFactory : ICompatibilityRuleFactory
{
    private readonly Lazy<IEnumerable<ICompatibilityRule>> _rules;
    public CompatibilityRuleFactory()
    {
        _rules = new Lazy<IEnumerable<ICompatibilityRule>>(() =>
        [
            new CpuMotherboardSocketRule(),
            new CpuCoolerTdpRule(),
            new RamCpuSpeedRule(),
            new RamMotherboardTypeRule(),
            new RamMotherboardCapacityRule(),
            new RamMotherboardSpeedRule(),
            new RamMotherboardSlotsRule(),
            new StorageMotherboardM2CountRule(),
            new StorageMotherboardSataCountRule(),
            new StorageM2InterfaceWarningRule(),
            new GpuCaseLengthRule(),
            new GpuCaseWidthSlotsWarningRule(),
            new GpuPsuPowerConnectorsRule(),
            new TotalPowerConsumptionRule(),
            new StoragePsuSataConnectorsRule(),
            new MotherboardCaseFormFactorRule(),
            new CoolerCaseAirHeightRule(),
            new CoolerCaseLiquidRadiatorRule(),
            new CoolerCpuSocketSupportRule(),
            new GpuMotherboardPcieVersionWarningRule(),
            new CpuGpuPriceBalanceWarningRule(),
            new RamSameTypeRule()
    ]);
    }
    public IEnumerable<ICompatibilityRule> CreateRules() => _rules.Value;
}