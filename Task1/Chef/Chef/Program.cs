using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chef.Assistants;
using Chef.Cook;
using Chef.Cook.Ingredients;
using Chef.Cook.Ingredients.Base;
using Chef.Cook.Units;
using Chef.Cook.Units.Interfaces;
using Chef.Output;

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

            //ISalad salad = new Salad(saladIngredients);

            var salad = saladAssistant.MakeSalad(saladIngredients);

            IOutput terminal = new Terminal();

            saladAssistant.SetOutput(terminal);
           
            saladAssistant.Print(salad);

            saladAssistant.PrintHelp();


            //saladAssistant.GetUserInput();

            bool breakFlag = true;
            while (breakFlag)
            {
                ISalad result;
                switch (saladAssistant.GetUserInput())
                {
                    case TerminalCommands.Sort + TerminalCommands.OnCaloricContent:
                        result = saladAssistant.SortByName();
                        saladAssistant.Print(result);
                        break;
                    case TerminalCommands.Sort + TerminalCommands.OnIngredientName:
                        result = saladAssistant.SortByName();
                        saladAssistant.Print(result);
                        break;
                    case TerminalCommands.SearchOnCaloricContentRange:
                        break;
                    case TerminalCommands.Exit:
                        breakFlag = false;
                        break;
                    default:
                        continue;
                }
                //terminal.Print();
            }



        }

        //private static string GetSortedIngredients(string[] input)
        //{
        //    if (input.Length == 2)
        //    {
        //        switch (input[1])
        //        {
        //            case CaloricContent100:
        //                var k100Sort = new Salad<Vegetable>(salad
        //                    .OrderBy(i => i.CaloricContentPer100Gram));
        //                return k100Sort.ToConsoleStr();
        //            case CaloricContent:
        //                var kSort = new Salad<Vegetable>(salad
        //                    .OrderBy(i => i.CaloricContent));
        //                return kSort.ToConsoleStr();
        //            case Weight:
        //                var wSort = new Salad<Vegetable>(salad
        //                    .OrderBy(i => i.Weight));
        //                return wSort.ToConsoleStr();
        //            default:
        //                return "Неправильный параметр сортировки";
        //        }
        //    }
        //    else
        //    {
        //        return "Неправильный параметр сортировки";
        //    }
        //}

        private static (int bottom, int top) GetCaloricContentRange()
        {
            int bottom;
            int top;

            while (true)
            {
                Console.WriteLine("Введите нижнею границу диапазона");
                var input = Console.ReadLine();
                if (int.TryParse(input, System.Globalization.NumberStyles.Number,
                    System.Globalization.CultureInfo.InvariantCulture, out bottom))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный ввод");
                }
            }

            while (true)
            {
                Console.WriteLine("Введите верхнюю границу диапазона");
                var input = Console.ReadLine();

                if (int.TryParse(input, System.Globalization.NumberStyles.Number,
                    System.Globalization.CultureInfo.InvariantCulture, out top))
                {
                    if (bottom > top)
                    {
                        Console.WriteLine("Верхняя граница должна быть больше нижней");
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Неверный ввод");
                }
            }

            return (bottom, top);
        }

        //private static string GetIngredientsForCaloricContentRange(int bottom, int top)
        //{
        //    var result = new Salad<Vegetable>(salad
        //        .Where(i => i.CaloricContent >= bottom && i.CaloricContent <= top)
        //        .OrderBy(i => i.CaloricContent));
        //    return result.ToConsoleStr();
        //}

    }
}


