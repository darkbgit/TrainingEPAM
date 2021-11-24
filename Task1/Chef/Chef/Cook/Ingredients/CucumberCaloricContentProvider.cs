using Chef.Cook.Ingredients.Base;
using Chef.Cook.Units.Interfaces;

namespace Chef.Cook.Ingredients
{
    public class CucumberCaloricContentProvider : ICaloricContentProvider
    {
        private const double CUCUMBER_CALORIC_CONTENT_PER_GRAM = 0.16;

        private readonly IUnit _unit;

        public CucumberCaloricContentProvider(IWeight unit)
        {
            _unit = unit;
        }

        public double GetCaloricContent()
        {
            return _unit.ToBaseUnit() * CUCUMBER_CALORIC_CONTENT_PER_GRAM;
        }
    }
}
