using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chef.Cook;
using Chef.Cook.Ingredients;
using Chef.Cook.Ingredients.Base;
using Chef.Cook.Units;
using Chef.Cook.Units.Interfaces;

namespace Chef
{
    class Program
    {
        const string Sort = "s";
        const string CaloricContent100 = "k100";
        const string CaloricContent = "k";
        const string Weight = "w";
        const string SearchCaloricContentRange = "c";
        const string Exit = "e";


        static void Main(string[] args)
        {
            Ingredient cucumber = new Cucumber();
            IWeight cucumberUnit = new Gram();
            ICaloricContentProvider cucumberCaloricContentProvider = new CucumberCaloricContentProvider(cucumberUnit);

            Ingredient tomato = new Tomato();
            IPiece tomatoUnit = new Piece("шт", 50);
            ICaloricContentProvider tomatoCaloricContentProvider = new TomatoCaloricContentProvider(tomatoUnit);

            Ingredient oliveOil = new OliveOil();
            IVolume oliveOilUnit 

            Ingredient salt = new Salt();
            IVolume saltUnit = new TeaSpoon();
            ICaloricContentProvider solCaloricContentProvider = new SaltCaloricContentProvider();
            
            IEnumerable<SaladIngredient> saladIngredients = new List<SaladIngredient>
            {
                new SaladIngredient(cucumber, cucumberCaloricContentProvider, cucumberUnit.ToString(), 150),
                new SaladIngredient(cucumber, tomatoCaloricContentProvider, tomatoUnit.ToString(), 2)
            };


            var salad = new Salad(saladIngredients);

            //Console.WriteLine(salad.ToConsoleStr());

            bool breakFlag = true;

            while (breakFlag)
            {
                Console.WriteLine($"Для сортировки по свойству введите \"-{Sort}");
                Console.WriteLine($"                                       -{CaloricContent100}\" - ККалорий в 100 грамм продукта");
                Console.WriteLine($"                                       -{CaloricContent}\" - ККалорий в продукте");
                Console.WriteLine($"                                       -{Weight}\" - вес продукта");
                Console.WriteLine($"Для поиска ингредиентов по калорийности введите \"-{SearchCaloricContentRange}\"");
                Console.WriteLine($"Для выхода введите \"-{Exit}\"");
                var input = Console.ReadLine()?
                    .Split('-')
                    .Where(p => !string.IsNullOrWhiteSpace(p))
                    .Select(p => p.Trim())
                    .ToArray();

                if (input != null && input.Any())
                {
                    switch (input[0])
                    {
                        case Sort:
                            //Console.WriteLine(GetSortedIngredients(input));
                            break;
                        case SearchCaloricContentRange:
                            (int bottom, int top) = GetCaloricContentRange();
                            //Console.WriteLine(GetIngredientsForCaloricContentRange(bottom, top));
                            break;
                        case Exit:
                            breakFlag = !breakFlag;
                            break;
                        default:
                            Console.WriteLine("Неопознанная команда");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Неопознанная команда");
                }
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


