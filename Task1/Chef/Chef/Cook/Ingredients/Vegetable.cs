namespace Chef.Cook.Ingredients
{
    public abstract class Vegetable
    {
        public string Name { get; protected init; }
        public double CaloricContentPer100Gram { get; protected init; }
        public double Weight { get; }

        public double CaloricContent => CaloricContentPer100Gram * Weight / 100;

        protected Vegetable(double weight)
        {
            Weight = weight;
        }
    }
}
