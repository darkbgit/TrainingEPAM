using Chef.Cook.Ingredients.Base;

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
