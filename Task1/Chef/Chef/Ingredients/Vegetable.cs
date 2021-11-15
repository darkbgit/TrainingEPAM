namespace Chef.Ingredients
{
    public abstract class Vegetable
    {
        public string Name { get; set; }
        public double CaloricContentPer100Gramm { get; set; }
        public double Weight { get; set; }

        public double CaloricContent => CaloricContentPer100Gramm * Weight / 100;

        protected Vegetable(double weight)
        {
            Weight = weight;
        }
    }
}
