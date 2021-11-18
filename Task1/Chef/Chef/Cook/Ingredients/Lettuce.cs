namespace Chef.Cook.Ingredients
{
    public class Lettuce : Vegetable
    {
        public Lettuce(double weight) 
            : base(weight)
        {
            Name = "Салат-латук";
            CaloricContentPer100Gram = 15;
        }
    }
}
