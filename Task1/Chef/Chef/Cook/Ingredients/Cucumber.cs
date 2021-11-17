namespace Chef.Cook.Ingredients
{
    public class Cucumber : Vegetable
    {
        public Cucumber(double weight) 
            : base(weight)
        {
            Name = "Огурец";
            CaloricContentPer100Gram = 16;
        }
    }
}
