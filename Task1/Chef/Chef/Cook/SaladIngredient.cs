using Chef.Cook.Ingredients.Base;


namespace Chef.Cook
{
    public class SaladIngredient
    {
        private readonly ICaloricContentProvider _caloricContentProvider;

        public SaladIngredient(Ingredient ingredient, ICaloricContentProvider caloricContentProvider, string unitName, double numberOfUnits)
        {
            _caloricContentProvider = caloricContentProvider;
            Ingredient = ingredient;
            NumberOfUnits = numberOfUnits;
            UnitName = unitName;
        }

        public Ingredient Ingredient { get; }

        public double CaloricContentPerUnit => _caloricContentProvider.GetCaloricContent();

        public double NumberOfUnits { get; }

        public string UnitName { get; }

        public double CaloricContent => CaloricContentPerUnit * NumberOfUnits;



    }
}
