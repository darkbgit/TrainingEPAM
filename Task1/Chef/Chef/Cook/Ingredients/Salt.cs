using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chef.Cook.Ingredients.Base;

namespace Chef.Cook.Ingredients
{
    public class Salt : Ingredient
    {
        private const string SALT_NAME = "Соль";
        public Salt() 
            : base(SALT_NAME)
        {
        }
    }
}
