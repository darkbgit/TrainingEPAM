namespace Chef.Cook.Ingredients.Base
{
    public interface ICaloricContentProvider
    {
        double GetCaloricContent(UnitType unit);
    }
}