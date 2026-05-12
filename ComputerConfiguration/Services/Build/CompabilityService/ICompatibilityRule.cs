using ComputerConfiguration.Models.Build;

namespace ComputerConfiguration.Services.Build.Compability;

public interface ICompatibilityRule
{
    CompatibilityRuleResult Check(ComputerBuild build);
}
