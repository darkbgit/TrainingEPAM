using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Cook
{
    public class Sorting
    {
        public IEnumerable<T> Sort<T, TKey>(IEnumerable<T> enumerable, Func<T, TKey> keySelector) where T : class
        {
            return enumerable.OrderBy(keySelector);
        }
    }
}
