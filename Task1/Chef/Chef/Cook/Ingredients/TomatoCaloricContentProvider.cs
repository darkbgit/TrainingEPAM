using Chef.Cook.Ingredients.Base;
using Chef.Cook.Units.Interfaces;

namespace Chef.Cook.Ingredients
{
    public class TomatoCaloricContentProvider : ICaloricContentProvider
    {
        private const double CUCUMBER_CALORIC_CONTENT_PER_GRAM = 0.199;

        private readonly IUnit _unit;

        public TomatoCaloricContentProvider(IWeight unit)
        {
            _unit = unit;
        }

        public TomatoCaloricContentProvider(IPiece unit)
        {
            _unit = unit;
        }

        public double GetCaloricContent()
        {
            return _unit.ToBaseUnit() * CUCUMBER_CALORIC_CONTENT_PER_GRAM;
        }
    }
}
