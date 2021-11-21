using Chef.Cook.Ingredients.Base;

namespace Chef.Cook.Ingredients
{
    public class Tomato : Ingredient
    {
        public Tomato(double weight) : base(weight)
        {
            Name = "Помидор";
            CaloricContentPer100Gram = 19.9;
        }
    }
}
