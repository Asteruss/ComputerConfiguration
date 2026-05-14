using ComputerConfiguration.DTO;

namespace ComputerConfiguration.Services.Build.Compability;

public interface ICompatibilityRule
{
    CompatibilityRuleResult Check(ComputerBuildDTO build);
}
