using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chef.Cook.Ingredients;
using Chef.Cook.Ingredients.Base;

namespace Chef.Cook
{
    public class Salad : ISalad

    {
        private readonly List<SaladIngredient> _saladIngredients;

        //public Salad()
        //{
        //    _saladIngredients = new List<SaladIngredient>();
        //}

        public Salad(IEnumerable<SaladIngredient> saladIngredients)
        {
            _saladIngredients = saladIngredients.ToList();
        }

        public IEnumerable<SaladIngredient> Ingredients => _saladIngredients;

        public double SumOfCaloricContent() => _saladIngredients.Sum(i => i.CaloricContent);


        //public void Add(T item)
        //{
        //    _saladIngredients.Add(item);
        //}


        //public IEnumerable<SaladIngredient> Sort(Func<SaladIngredient, TKey> keySelector)
        //{
        //    return _saladIngredients.OrderBy()
        //}


        public IEnumerator<SaladIngredient> GetEnumerator()
        {
            return ((IEnumerable<SaladIngredient>) _saladIngredients).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _saladIngredients).GetEnumerator();
        }
    }
}
