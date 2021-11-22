using Chef.Cook.Ingredients.Base;
using Chef.Cook.Units;

namespace Chef.Cook.Ingredients
{
    public class Cucumber : Ingredient
    {
        private const string CUCUMBER_NAME = "Огурец";

        public Cucumber() 
            : base(CUCUMBER_NAME)
        {
        }

    }
}
