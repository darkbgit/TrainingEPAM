using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Output
{
    public class CommandLine : ICommandLine
    {
        private const string SORT_ON_CALORIC_CONTENT = "--s-k";
        private const string SORT_ON_INGREDIENT_NAME = "n";
        private const string SEARCH_ON_CALORIC_CONTENT_RANGE = "c";
        private const string EXIT = "--e";

        public CommandLineCommand CommandLineArgumentParser(string[] args)
        {
            return args.Length switch
            {
                0 => CommandLineCommand.Base,
                1 => args[0] switch
                {
                    CommandLineArguments.PrintInitialData => CommandLineCommand.PrintData,
                    CommandLineArguments.Exit => CommandLineCommand.Exit,
                    CommandLineArguments.SortOnCaloricContent => CommandLineCommand.SortOnCaloricContent,
                    CommandLineArguments.SortOnIngredientName => CommandLineCommand.SortOnIngredientName,
                    _ => CommandLineCommand.UndefinedCommand
                },
                3 => args[0] switch
                {
                    CommandLineArguments.SearchOnCaloricContentRange => CommandLineCommand.SearchOnCaloricContentRange,
                    _ => CommandLineCommand.UndefinedCommand
                },
                _ => CommandLineCommand.UndefinedCommand
            };
        }

        public (int bottom, int top) GetUserCaloricContentRange(string[] args)
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


        public string[] GetArguments()
        {
            var line = Console.ReadLine();
            return Console.ReadLine()
                ?.Split(' ')
                .ToArray();
        }

    }
}
