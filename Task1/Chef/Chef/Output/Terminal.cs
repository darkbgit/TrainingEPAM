using Chef.Cook;
using System;
using System.Linq;
using System.Text;

namespace Chef.Output
{
    public class Terminal : IOutput
    {
        private readonly string _helpString;

        public Terminal()
        {
            _helpString = GenerateHelpString();
        }

        public void Print(string str)
        {
            Console.WriteLine(str);
        }

        public void Print(ISalad salad)
        {

            const int COLUMN_COUNT = 4;
            const int NAME_WIDTH = 20;
            const int CALORIC_CONTENT_PER_UNIT_WIDTH = 20;
            const int UNIT_NAME_WIDTH = 20;
            const int NUMBER_OF_UNITS_WIDTH = 10;
            const int CALORIC_CONTENT_WIDTH = 10;
            const int WEIGHT_WIDTH = 10;
            const int TOTAL_WIDTH = NAME_WIDTH + CALORIC_CONTENT_PER_UNIT_WIDTH + CALORIC_CONTENT_WIDTH + WEIGHT_WIDTH +
                COLUMN_COUNT - 1;


            var builder = new StringBuilder();
            builder.Append('_', TOTAL_WIDTH);
            builder.Append(Environment.NewLine);
            builder.Append($"{"Ингредиент",NAME_WIDTH}");
            builder.Append('|');
            builder.Append($"{"Единицы измерения",UNIT_NAME_WIDTH}");
            builder.Append('|');
            builder.Append($"{"Количество",NUMBER_OF_UNITS_WIDTH}");
            builder.Append('|');
            builder.Append($"{"ККал",CALORIC_CONTENT_WIDTH}");
            builder.Append(Environment.NewLine);
            builder.Append('_', TOTAL_WIDTH);
            builder.Append(Environment.NewLine);

            foreach (var ingredient in salad)
            {
                builder.Append($"{ingredient.Ingredient.Name,NAME_WIDTH}");
                builder.Append('|');
                builder.Append($"{ingredient.UnitName,UNIT_NAME_WIDTH}");
                builder.Append('|');
                builder.Append($"{ingredient.NumberOfUnits,NUMBER_OF_UNITS_WIDTH}");
                builder.Append('|');
                builder.Append($"{ingredient.CaloricContent,CALORIC_CONTENT_WIDTH:f2}");
                builder.Append(Environment.NewLine);
            }

            builder.Append('_', TOTAL_WIDTH);
            builder.Append(Environment.NewLine);
            builder.Append("Общая калорийность ");
            builder.Append($"{salad.SumOfCaloricContent():f2}");
            builder.Append(" ККал");
            builder.Append(Environment.NewLine);
            builder.Append('_', TOTAL_WIDTH);

            Console.WriteLine(builder.ToString());
        }

        public void PrintHelp()
        {
            Console.WriteLine(_helpString);
        }

        public string GetUserInput()
        {
            var input = Console.ReadLine()?
                .Split('-')
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Select(p => p.Trim())
                .ToArray();

            if (input != null && input.Any() && input.Length <= 2)
            {
                switch (input[0])
                {
                    case TerminalCommands.Sort:
                        if (input.Length == 2)
                        {
                            switch (input[1])
                            {
                                case TerminalCommands.OnCaloricContent:
                                    return TerminalCommands.Sort + TerminalCommands.OnCaloricContent;
                                case TerminalCommands.OnIngredientName:
                                    return TerminalCommands.Sort + TerminalCommands.OnIngredientName;
                                default:
                                    Console.WriteLine("Неправильный параметр сортировки");
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Неправильный параметр сортировки");
                        }
                        break;
                    case TerminalCommands.SearchOnCaloricContentRange:
                        return TerminalCommands.SearchOnCaloricContentRange;
                    case TerminalCommands.Exit:
                        return TerminalCommands.Exit;
                    default:
                        Console.WriteLine("Неопознанная команда");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Неопознанная команда");
            }

            return string.Empty;
        }

        public (int bottom, int top ) GetUserCaloricContentRange()
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

        private static string GenerateHelpString()

        {
            var builder = new StringBuilder();

            builder.AppendLine($"Для сортировки по свойству введите \"-{TerminalCommands.Sort}");
            var firstLength = builder.Length;
            builder.Append(' ', firstLength);
            builder.AppendLine($"-{TerminalCommands.OnIngredientName}\" - название продукта");
            builder.Append(' ', firstLength);
            builder.AppendLine($"-{TerminalCommands.OnCaloricContent}\" - ККалорий в продукте");
            builder.AppendLine(
                $"Для поиска ингредиентов по калорийности введите \"-{TerminalCommands.SearchOnCaloricContentRange}\"");
            builder.AppendLine($"Для выхода введите \"-{TerminalCommands.Exit}\"");

            return builder.ToString();
        }
    }
}
