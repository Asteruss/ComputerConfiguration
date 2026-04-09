namespace ComputerConfiguration.Filters.Strategies;

public class ExactMatchStrategy : IMatchStrategy
{
    public bool IsMatch(object propertyValue, object filterValue)
    {
        if (propertyValue == null && filterValue == null) return true;
        if (propertyValue == null || filterValue == null) return false;
        return propertyValue.Equals(filterValue);
    }
}
