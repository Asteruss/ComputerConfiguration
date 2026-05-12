using ComputerConfiguration.Services.Build;
using ComputerConfiguration.Models.Enums;

namespace ComputerConfiguration.DTO;

public class CompabilityErrorDTO
{
    public string TypeString { get; set; }
    public string Message { get; set; }
    public bool IsError { get; set; } = false;
    public bool IsWarning { get; set; } = false;
    public bool IsNotFound { get; set; } = false;
    public CompabilityErrorDTO(CompatibilityRuleEnum type, string message)
    {
        Message = message;
        if (type == CompatibilityRuleEnum.Error)
        {
            IsError = true;
            TypeString = "Ошибка";
        }
        else if (type == CompatibilityRuleEnum.Warning)
        {
            IsWarning = true;
            TypeString = "Предупреждение";
        }
        else if (type == CompatibilityRuleEnum.ComponentNotFound)
        {
            IsNotFound = true;
            TypeString = "Отсуствие компонента";

        }
    }

}
