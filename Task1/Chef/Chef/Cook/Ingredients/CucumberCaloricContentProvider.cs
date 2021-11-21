using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chef.Cook.Ingredients.Base;

namespace Chef.Cook.Ingredients
{
    public class CucumberCaloricContentProvider : ICaloricContentProvider
    {
        private const double CUCUMBER_CALORIC_CONTENT_PER_GRAM = 0.16;
        public double GetCaloricContent(UnitType unit)
        {
            return unit switch
            {
                UnitType.TeaSpoon => 1,
                UnitType.Tablespoon => 2,
                UnitType.Glass => 3,
                UnitType.Piece => 4,
                UnitType.Milligram => 0.001,
                UnitType.Gram => 1,
                UnitType.Kilogram => 1000,
                _ => 0
            } * CUCUMBER_CALORIC_CONTENT_PER_GRAM;
        }
    }
}
