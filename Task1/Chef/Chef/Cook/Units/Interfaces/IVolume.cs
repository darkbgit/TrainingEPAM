namespace Chef.Cook.Units.Interfaces
{
    public interface IVolume : IUnit
    {
        double ToGram(Tablespoon.ConvertToGram convert);
    }
}
