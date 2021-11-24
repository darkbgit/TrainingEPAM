using System.Collections.Generic;

namespace Chef.Cook
{
    public interface ISalad : IEnumerable<SaladIngredient>
    {
        IEnumerable<SaladIngredient> Ingredients { get; }

        double SumOfCaloricContent();
    }
}
