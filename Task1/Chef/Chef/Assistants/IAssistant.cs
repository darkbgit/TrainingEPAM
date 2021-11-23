using System.Collections.Generic;
using Chef.Cook;
using Chef.Output;

namespace Chef.Assistants
{
    public interface IAssistant
    {
        void Sort();

        void Print(ISalad salad);

        void Print(string str);

        void PrintHelp();


        void GetUserInput();

        ISalad MakeSalad(IEnumerable<SaladIngredient> ingredients);

        void SetOutput(IOutput output);
    }
}
