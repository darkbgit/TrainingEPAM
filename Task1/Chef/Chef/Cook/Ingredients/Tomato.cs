namespace Chef.Cook.Ingredients
{
    public class Tomato : Vegetable
    {
        public Tomato(double weight) : base(weight)
        {
            Name = "Помидор";
            CaloricContentPer100Gram = 19.9;
        }
    }
}
