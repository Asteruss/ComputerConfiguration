using ComputerConfiguration.Models.Enums;

namespace ComputerConfiguration.Services.Build.Compability;


public class CompatibilityRuleResult
{
    public string ResultString { get; set; }
    public CompatibilityRuleEnum Result { get; set; }
    public CompatibilityRuleResult(CompatibilityRuleEnum result, string resultString)
    {
        ResultString = resultString;
        Result = result;
    }
    public CompatibilityRuleResult()
    {
        ResultString = "";
        Result = CompatibilityRuleEnum.Good;
    }
}

public class CompatibilityResult
{
    public List<string> Errors { get; } = new();
    public List<string> Warnings { get; } = new();
    public List<string> NotFoundComponents { get; } = new();
    public bool IsCompatible => Errors.Count == 0;
}

