using System.ComponentModel;
using System.Reflection;
namespace ComputerConfiguration.Extensions;

public static class EnumExtensions
{
    public static string ToDescriptionString(this Enum val)
    {
        FieldInfo field = val.GetType().GetField(val.ToString());
        DescriptionAttribute attribute = field?.GetCustomAttribute<DescriptionAttribute>();

        return attribute == null ? val.ToString() : attribute.Description;
    }
}
