using Chef.Ingredients;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Cooking
{
    public class SaladIngredients : ICollection<Vegetable>
    {
        private List<Vegetable> _saladIngredients;

        public SaladIngredients()
        {
            _saladIngredients = new List<Vegetable>();
        }
        

        public int Count => _saladIngredients.Count;

        public bool IsReadOnly => ((ICollection<Vegetable>)_saladIngredients).IsReadOnly;

        public void Add(Vegetable item)
        {
            _saladIngredients.Add(item);
        }

        public void Clear()
        {
            _saladIngredients.Clear();
        }

        public bool Contains(Vegetable item)
        {
            return _saladIngredients.Contains(item);
        }

        public void CopyTo(Vegetable[] array, int arrayIndex)
        {
            _saladIngredients.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Vegetable> GetEnumerator()
        {
            return ((IEnumerable<Vegetable>)_saladIngredients).GetEnumerator();
        }

        public bool Remove(Vegetable item)
        {
            return _saladIngredients.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_saladIngredients).GetEnumerator();
        }




    }
}
