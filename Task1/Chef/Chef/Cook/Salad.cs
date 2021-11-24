using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Chef.Cook
{
    public class Salad : ISalad

    {
        private readonly List<SaladIngredient> _saladIngredients;

        public Salad(IEnumerable<SaladIngredient> saladIngredients)
        {
            _saladIngredients = saladIngredients.ToList();
        }

        public IEnumerable<SaladIngredient> Ingredients => _saladIngredients;

        public double SumOfCaloricContent() => _saladIngredients.Sum(i => i.CaloricContent);

        public IEnumerator<SaladIngredient> GetEnumerator()
        {
            return ((IEnumerable<SaladIngredient>)_saladIngredients).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_saladIngredients).GetEnumerator();
        }
    }
}
