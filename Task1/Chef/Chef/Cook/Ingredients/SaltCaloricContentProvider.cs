using Chef.Cook.Ingredients.Base;

namespace Chef.Cook.Ingredients
{
    public class SaltCaloricContentProvider : ICaloricContentProvider
    {
        private const double SALT_CALORIC_CONTENT_PER_UNIT = 0;
        public double GetCaloricContent()
        {
            return SALT_CALORIC_CONTENT_PER_UNIT;
        }
    }
}
