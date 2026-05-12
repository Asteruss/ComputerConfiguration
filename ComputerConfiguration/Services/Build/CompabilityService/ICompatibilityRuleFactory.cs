namespace ComputerConfiguration.Services.Build.Compability;


public interface ICompatibilityRuleFactory
{
    IEnumerable<ICompatibilityRule> CreateRules();
}

