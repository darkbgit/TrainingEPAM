using System;
using System.Collections.Generic;
using System.Linq;
using Chef.Cook;

namespace Chef.SaladExtensions
{
    public static class SaladExtension
    {
        public static IEnumerable<T> Sort<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector) where T : SaladIngredient
        {
            return enumerable.OrderBy(keySelector);
        }

        public static IEnumerable<SaladIngredient> GetRangeByCaloricContent(this IEnumerable<SaladIngredient> enumerable, int bottomBorder, int topBorder)
        {
            return enumerable.Where(i => i.CaloricContent >= bottomBorder && i.CaloricContent <= topBorder)
                .OrderBy(i => i.CaloricContent);
        }
    }
}
