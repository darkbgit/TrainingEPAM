using Chef.Cook;
using Chef.Output;
using Chef.SaladExtensions;
using System.Collections.Generic;

namespace Chef.Assistants
{
    public class SaladAssistant : IAssistant
    {

        public ISalad SortByName(ISalad salad)
        {
            return new Salad(salad.Sort(i => i.Ingredient.Name));
        }

        public ISalad SortByCaloricContent(ISalad salad)
        {
            return new Salad(salad.Sort(i => i.CaloricContent));
        }

        public ISalad SearchOnCaloricContentRange(ISalad salad, int bottom, int top)
        {
            return new Salad(salad.GetRangeByCaloricContent(bottom, top));
        }

        public ISalad MakeSalad(IEnumerable<SaladIngredient> ingredients)
        {
            return new Salad(ingredients);
        }
    }
}
