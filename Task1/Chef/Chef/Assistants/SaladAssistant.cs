using System;
using System.Collections.Generic;
using Chef.Cook;
using Chef.Output;

namespace Chef.Assistants
{
    public class SaladAssistant : IAssistant
    {
        private IOutput _output;
        private  ISalad _salad;

        private Sorting _sorting;

        public ISalad SortByName()
        {
            _sorting = new Sorting();

            return new Salad(_sorting.Sort(_salad, i => i.Ingredient.Name));

            
        }

        public void Print(ISalad salad)
        {
            _output.Print(salad);

        }

        public void Print(string str)
        {
            _output.Print(str);
        }

        public void PrintHelp()
        {
            _output.PrintHelp();
        }

        public string GetUserInput()
        {
            return _output.GetUserInput();
        }

        public ISalad MakeSalad(IEnumerable<SaladIngredient> ingredients)
        {
            _salad = new Salad(ingredients);
            return new Salad(ingredients);
        }

        public void SetOutput(IOutput output)
        {
            _output = output;
        }

    }
}
