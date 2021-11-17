using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chef.Cook.Ingredients;

namespace Chef.Cook
{
    public class Salad<T> : IEnumerable<T> where T : Vegetable

    {
        private readonly List<T> _saladIngredients;

        public Salad()
        {
            _saladIngredients = new List<T>();
        }

        public Salad(IEnumerable<T> saladIngredients)
        {
            _saladIngredients = saladIngredients.ToList();
        }

        public double SumOfCaloricContent() => _saladIngredients.Sum(i => i.CaloricContent);


        public void Add(T item)
        {
            _saladIngredients.Add(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>) _saladIngredients).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _saladIngredients).GetEnumerator();
        }
    }
}
