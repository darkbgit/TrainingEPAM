using Chef.Cook;

namespace Chef.Output
{
    public interface IOutput
    {
        void Print(string str);

        void Print(ISalad salad);

        void PrintHelp();
    }
}
