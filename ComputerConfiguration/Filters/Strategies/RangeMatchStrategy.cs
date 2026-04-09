using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Filters.Strategies;

public class RangeMatchStrategy : IMatchStrategy
{
    public bool IsMatch(object propertyValue, object filterValue)
    {
        if (propertyValue == null || filterValue == null) return false;

        if (filterValue is ValueTuple<double, double> range)
        {
            double min = range.Item1;
            double max = range.Item2;

            if (propertyValue is double d)
                return d >= min && d <= max;
            if (propertyValue is int i)
                return i >= min && i <= max;
        }
        return false;
    }
}
