using Chef.Cook;
using System.Collections.Generic;

namespace Chef.Assistants
{
    public interface IAssistant
    {
        ISalad SortByName(ISalad salad);

        ISalad SortByCaloricContent(ISalad salad);

        ISalad SearchOnCaloricContentRange(ISalad salad);

        void Print(ISalad salad);

        void Print(string str);

        string GetUserInput();

        ISalad MakeSalad(IEnumerable<SaladIngredient> ingredients);

    }
}
