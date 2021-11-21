using Chef.Cook.Ingredients.Base;

namespace Chef.Cook.Ingredients
{
    public class Lettuce : Ingredient
    {
        public Lettuce(double weight) 
            : base(weight)
        {
            Name = "Салат-латук";
            CaloricContentPer100Gram = 15;
        }
    }
}
