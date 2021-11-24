using Chef.Cook;
using Chef.Output;
using Chef.SaladExtensions;
using System.Collections.Generic;

namespace Chef.Assistants
{
    public class SaladAssistant : IAssistant
    {
        private readonly IOutput _output;

        public SaladAssistant(IOutput output)
        {
            _output = output;
        }

        public ISalad SortByName(ISalad salad)
        {
            return new Salad(salad.Sort(i => i.Ingredient.Name));
        }

        public ISalad SortByCaloricContent(ISalad salad)
        {
            return new Salad(salad.Sort(i => i.CaloricContent));
        }

        public ISalad SearchOnCaloricContentRange(ISalad salad)
        {
            (int bottom, int top) = _output.GetUserCaloricContentRange();
            return new Salad(salad.GetRangeByCaloricContent(bottom, top));
        }

        public void Print(ISalad salad)
        {
            _output.Print(salad);
            _output.PrintHelp();
        }

        public void Print(string str)
        {
            _output.Print(str);
        }

        public string GetUserInput()
        {
            return _output.GetUserInput();
        }

        public ISalad MakeSalad(IEnumerable<SaladIngredient> ingredients)
        {
            return new Salad(ingredients);
        }
    }
}
