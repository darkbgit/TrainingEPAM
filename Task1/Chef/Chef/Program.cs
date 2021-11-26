using Chef.Assistants;
using Chef.Cook;
using Chef.Cook.Ingredients;
using Chef.Cook.Ingredients.Base;
using Chef.Cook.Units;
using Chef.Cook.Units.Interfaces;
using Chef.Output;
using System;
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

            IAssistant saladAssistant = new SaladAssistant();

            var salad = saladAssistant.MakeSalad(saladIngredients);

            IOutput terminal = new Terminal();

            ICommandLine commandLine = new CommandLine();

            bool breakFlag = true;
            while (breakFlag)
            {
                switch (commandLine.CommandLineArgumentParser(args))
                {
                    case CommandLineCommand.PrintData:
                        terminal.Print(salad);
                        break;
                    case CommandLineCommand.SortOnCaloricContent:
                        terminal.Print(saladAssistant.SortByCaloricContent(salad));
                        break;
                    case CommandLineCommand.SortOnIngredientName:
                        terminal.Print(saladAssistant.SortByName(salad));
                        break;
                    case CommandLineCommand.SearchOnCaloricContentRange:
                        terminal.Print(saladAssistant.SearchOnCaloricContentRange(salad,
                            Convert.ToInt32(args[1]),
                            Convert.ToInt32(args[2])));
                        break;
                    case CommandLineCommand.Exit:
                        breakFlag = false;
                        continue;
                    case CommandLineCommand.Base:
                    case CommandLineCommand.UndefinedCommand:
                    default:
                        terminal.PrintHelp();
                        break;
                }
                args = commandLine.GetArguments();
            }
        }
    }
}


