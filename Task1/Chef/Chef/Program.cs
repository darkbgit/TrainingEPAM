using Chef.Assistants;
using Chef.Cook;
using Chef.Cook.Ingredients;
using Chef.Cook.Ingredients.Base;
using Chef.Cook.Units;
using Chef.Cook.Units.Interfaces;
using Chef.Output;
using System.Collections.Generic;

namespace Chef
{
    class Program
    {
        static void Main(string[] args)
        {
            Ingredient cucumber = new Cucumber();
            IWeight cucumberUnit = new Gram();
            ICaloricContentProvider cucumberCaloricContentProvider = new CucumberCaloricContentProvider(cucumberUnit);

            Ingredient tomato = new Tomato();
            IPiece tomatoUnit = new Piece("шт", 50);
            ICaloricContentProvider tomatoCaloricContentProvider = new TomatoCaloricContentProvider(tomatoUnit);

            Ingredient oliveOil = new OliveOil();
            IVolume oliveOilUnit = new Tablespoon();
            ICaloricContentProvider oliveOilCaloricContentProvider = new OliveOilCaloricContentProvider(oliveOilUnit);

            Ingredient salt = new Salt();
            IVolume saltUnit = new TeaSpoon();
            ICaloricContentProvider salCaloricContentProvider = new SaltCaloricContentProvider();

            IEnumerable<SaladIngredient> saladIngredients = new List<SaladIngredient>
            {
                new SaladIngredient(cucumber, cucumberCaloricContentProvider, cucumberUnit.ToString(), 150),
                new SaladIngredient(tomato, tomatoCaloricContentProvider, tomatoUnit.ToString(), 2),
                new SaladIngredient(oliveOil, oliveOilCaloricContentProvider, oliveOilUnit.ToString(), 3),
                new SaladIngredient(salt, salCaloricContentProvider, saltUnit.ToString(), 1)
            };

            IOutput terminal = new Terminal();

            IAssistant saladAssistant = new SaladAssistant(terminal);

            var salad = saladAssistant.MakeSalad(saladIngredients);

            saladAssistant.Print(salad);

            bool breakFlag = true;
            while (breakFlag)
            {
                ISalad result;
                switch (saladAssistant.GetUserInput())
                {
                    case TerminalCommands.Sort + TerminalCommands.OnCaloricContent:
                        result = saladAssistant.SortByCaloricContent(salad);
                        break;
                    case TerminalCommands.Sort + TerminalCommands.OnIngredientName:
                        result = saladAssistant.SortByName(salad);
                        break;
                    case TerminalCommands.SearchOnCaloricContentRange:
                        result = saladAssistant.SearchOnCaloricContentRange(salad);
                        break;
                    case TerminalCommands.Exit:
                        breakFlag = false;
                        continue;
                    default:
                        continue;
                }
                saladAssistant.Print(result);
            }
        }
    }
}


