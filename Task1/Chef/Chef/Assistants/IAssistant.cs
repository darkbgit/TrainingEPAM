using System;
using System.Collections.Generic;
using Chef.Cook;
using Chef.Output;

namespace Chef.Assistants
{
    public interface IAssistant
    {
        ISalad SortByName();

        void Print(ISalad salad);

        void Print(string str);

        void PrintHelp();


        string GetUserInput();

        ISalad MakeSalad(IEnumerable<SaladIngredient> ingredients);

        void SetOutput(IOutput output);
    }
}
