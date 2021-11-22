using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chef.Cook.Ingredients.Base;

namespace Chef.Cook.Ingredients
{
    public class OliveOil : Ingredient
    {
        private const string OLIVE_OIL_NAME = "Оливковое масло";
        public OliveOil()
            : base(OLIVE_OIL_NAME)
        {
        }
    }
}
