using Chef.Cook.Ingredients.Base;
using Chef.Cook.Units.Interfaces;

namespace Chef.Cook.Ingredients
{
    public class OliveOilCaloricContentProvider : ICaloricContentProvider
    {
        private const double OLIVE_OIL_CALORIC_CONTENT_PER_GRAM = 8.98;

        private const double DENSITY_ML_ON_GRAM = 0.915;

        private readonly IUnit _unit;
        private readonly double _densityCoefficient;

        public OliveOilCaloricContentProvider(IVolume unit)
        {
            _unit = unit;
            _densityCoefficient = DENSITY_ML_ON_GRAM;
        }

        public OliveOilCaloricContentProvider(IWeight unit, double densityCoefficient)
        {
            _unit = unit;
            _densityCoefficient = 1;
        }

        public double GetCaloricContent() => _unit.ToBaseUnit() * _densityCoefficient * OLIVE_OIL_CALORIC_CONTENT_PER_GRAM;
    }
}
