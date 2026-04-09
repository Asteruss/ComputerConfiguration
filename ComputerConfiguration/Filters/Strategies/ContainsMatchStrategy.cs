namespace ComputerConfiguration.Filters.Strategies;

public class ContainsMatchStrategy : IMatchStrategy
{
    public bool IsMatch(object propertyValue, object filterValue)
    {
        if (propertyValue == null || filterValue == null) return false;
        string prop = propertyValue.ToString();
        string filter = filterValue.ToString();
        return prop.Contains(filter, StringComparison.OrdinalIgnoreCase);
    }
}
