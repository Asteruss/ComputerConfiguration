using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Converters
{
    public static class ListToObservableCollection
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> lst) => new(lst);
        public static ObservableCollection<object> ToObservableCollectionObject<T>(this IEnumerable<T> lst) where T : class => new ObservableCollection<object>(lst);
    }

}
