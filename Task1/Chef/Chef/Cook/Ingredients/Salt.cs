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
