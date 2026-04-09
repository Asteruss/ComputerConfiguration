namespace ComputerConfiguration.Filters.Strategies;
public interface IMatchStrategy
{
    bool IsMatch(object propertyValue, object filterValue);
}
