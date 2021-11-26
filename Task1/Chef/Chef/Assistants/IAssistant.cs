using Chef.Cook;
using System.Collections.Generic;

namespace Chef.Assistants
{
    public interface IAssistant
    {
        ISalad SortByName(ISalad salad);

        ISalad SortByCaloricContent(ISalad salad);

        ISalad SearchOnCaloricContentRange(ISalad salad, int bottom, int top);

        ISalad MakeSalad(IEnumerable<SaladIngredient> ingredients);

    }
}
