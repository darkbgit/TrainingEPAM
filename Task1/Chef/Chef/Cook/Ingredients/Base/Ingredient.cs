using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Cook.Ingredients.Base
{
    public abstract class Ingredient
    {
        private readonly ICaloricContentProvider _caloricContentProvider;

        protected Ingredient(string name)
        {
            Name = name;
        }

        protected Ingredient(string name, ICaloricContentProvider caloricContentProvider)
        {
            Name = name;
            _caloricContentProvider = caloricContentProvider;
        }

        public string Name { get; protected init; }
    }
}
