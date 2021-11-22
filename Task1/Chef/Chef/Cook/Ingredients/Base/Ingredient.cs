using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Cook.Ingredients.Base
{
    public abstract class Ingredient
    {
        protected Ingredient(string name)
        {
            Name = name;
        }

        public string Name { get; protected init; }
    }
}
