using ComputerConfiguration.Models.Build;
using ComputerConfiguration.Models.Enums;

namespace ComputerConfiguration.Services.Build.Compability;

public class CompatibilityCheckerService
{
    private readonly IEnumerable<ICompatibilityRule> _rules;
    public CompatibilityCheckerService(IEnumerable<ICompatibilityRule> rules) => _rules = rules;

    public CompatibilityResult Check(ComputerBuild build)
    {
        var result = new CompatibilityResult();
        foreach (var rule in _rules)
        {
            var res = rule.Check(build);
            if (res == null)
                continue;
            if (res.Result == CompatibilityRuleEnum.Error)
                result.Errors.Add(res.ResultString);
            else if (res.Result == CompatibilityRuleEnum.Warning)
                result.Warnings.Add(res.ResultString);
            else if (res.Result == CompatibilityRuleEnum.ComponentNotFound)
                result.NotFoundComponents.Add(res.ResultString);
        }
        return result;
    }
}
