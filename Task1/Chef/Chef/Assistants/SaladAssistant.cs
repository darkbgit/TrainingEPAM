using System;
using System.Collections.Generic;
using Chef.Cook;
using Chef.Output;

namespace Chef.Assistants
{
    public class SaladAssistant : IAssistant
    {
        private IOutput _output;
        public void Sort()
        {
            throw new NotImplementedException();
        }

        public void Print(ISalad salad)
        {
            _output?.Print(salad);

        }

        public void Print(string str)
        {
            _output.Print(str);
        }

        public void PrintHelp()
        {
            _output.PrintHelp();
        }

        public void GetUserInput()
        {
            throw new NotImplementedException();
        }

        public ISalad MakeSalad(IEnumerable<SaladIngredient> ingredients)
        {
            return new Salad(ingredients);
        }

        public void SetOutput(IOutput output)
        {
            _output = output;
        }

        public void MakeSalad()
        {
            throw new NotImplementedException();
        }
    }
}
