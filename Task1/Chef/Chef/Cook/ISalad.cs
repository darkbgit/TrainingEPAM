using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Cook
{
    public interface ISalad : IEnumerable<SaladIngredient>
    {
        IEnumerable<SaladIngredient> Ingredients { get; }

        double SumOfCaloricContent();
    }
}
