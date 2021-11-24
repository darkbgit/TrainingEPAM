using Chef.Cook.Ingredients.Base;

namespace Chef.Cook.Ingredients
{
    public class Tomato : Ingredient
    {
        private const string TOMATO_NAME = "Помидор";
        public Tomato() : base(TOMATO_NAME)
        {
        }
    }
}
