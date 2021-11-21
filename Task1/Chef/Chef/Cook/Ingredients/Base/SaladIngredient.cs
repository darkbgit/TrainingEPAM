namespace Chef.Cook.Ingredients.Base
{
    public class SaladIngredient
    {
        private readonly ICaloricContentProvider _caloricContentProvider;

        public SaladIngredient(Ingredient ingredient, ICaloricContentProvider caloricContentProvider,  UnitType unitType, double numberOfUnits)
        {
            _caloricContentProvider = caloricContentProvider;
            Ingredient = ingredient;
            NumberOfUnits = numberOfUnits;
            UnitType = unitType;
        }

        public Ingredient Ingredient { get; set; }

        public double CaloricContentPerUnit => _caloricContentProvider.GetCaloricContent(UnitType);

        public double NumberOfUnits { get; }

        public UnitType UnitType { get; set; }

        public double CaloricContent => CaloricContentPerUnit * NumberOfUnits;
 


    }
}
