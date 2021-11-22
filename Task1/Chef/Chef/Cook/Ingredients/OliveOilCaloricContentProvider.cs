using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chef.Cook.Ingredients.Base;
using Chef.Cook.Units.Interfaces;

namespace Chef.Cook.Ingredients
{
    public class OliveOilCaloricContentProvider : ICaloricContentProvider
    {
        private const double OLIVE_OIL_CALORIC_CONTENT_PER_GRAM = 8.98;

        private readonly IVolume _unit;

        public OliveOilCaloricContentProvider(IVolume unit)
        {
            _unit = unit;
        }

        public OliveOilCaloricContentProvider(IPiece unit)
        {
            //_unit = unit;
        }

        public double GetCaloricContent()
        {
            return _unit.ToGram(() =>  OLIVE_OIL_CALORIC_CONTENT_PER_GRAM * 2) * OLIVE_OIL_CALORIC_CONTENT_PER_GRAM;
        }
    }
}
