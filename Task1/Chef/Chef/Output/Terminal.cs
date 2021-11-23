using Chef.Cook;
using System;
using System.Linq;
using System.Text;

namespace Chef.Output
{
    public class Terminal : IOutput
    {

        public const string Sort = "s";
        public const string OnCaloricContentPerUnit = "u";
        public const string OnCaloricContent = "k";
        public const string OnWeight = "w";
        public const string SearchOnCaloricContentRange = "c";
        public const string Exit = "e";


        private string _helpString;

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
            builder.Append($"{"Ингредиент", NAME_WIDTH}");
            builder.Append('|');
            builder.Append($"{"Единицы измерения", UNIT_NAME_WIDTH}");
            builder.Append('|');
            builder.Append($"{"Количество", NUMBER_OF_UNITS_WIDTH}");
            builder.Append('|');
            builder.Append($"{"ККал", CALORIC_CONTENT_WIDTH}");
            builder.Append(Environment.NewLine);
            builder.Append('_', TOTAL_WIDTH);
            builder.Append(Environment.NewLine);

            foreach (var ingredient in salad)
            {
                builder.Append($"{ingredient.Ingredient.Name, NAME_WIDTH}");
                builder.Append('|');
                builder.Append($"{ingredient.UnitName, UNIT_NAME_WIDTH}");
                builder.Append('|');
                builder.Append($"{ingredient.NumberOfUnits, NUMBER_OF_UNITS_WIDTH}");
                builder.Append('|');
                builder.Append($"{ingredient.CaloricContent, CALORIC_CONTENT_WIDTH:f2}");
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
            _helpString = GenerateHelpString();
            Console.WriteLine(_helpString);
        }

        
        public string GetUserInput()
        {
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
                        switch (input[1])
                        {
                            case OnCaloricContent:
                                return Sort + OnCaloricContent;
                            case OnWeight:
                                return Sort + OnWeight;
                            case OnCaloricContentPerUnit:
                                return Sort + OnCaloricContentPerUnit;
                            default:
                                Console.WriteLine("Неправильный параметр сортировки");
                                break;
                        }
                        break;
                    case SearchOnCaloricContentRange:
                        return SearchOnCaloricContentRange;
                    case Exit:
                        return Exit;
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

        public (int top, int bottom) GetUserCaloricContentRange()
        {
            throw new NotImplementedException();
        }

        private static string GenerateHelpString()

        {
            var builder = new StringBuilder();

            builder.AppendLine($"Для сортировки по свойству введите \"-{Sort}");
            var firstLength = builder.Length;
            builder.Append(' ', firstLength);
            builder.AppendLine($"-{OnCaloricContentPerUnit}\" - ККалорий в 100 грамм продукта");
            builder.Append(' ', firstLength);
            builder.AppendLine($"-{OnCaloricContent}\" - ККалорий в продукте");
            builder.Append(' ', firstLength);
            builder.AppendLine($"-{OnWeight}\" - вес продукта");
            builder.AppendLine(
                $"Для поиска ингредиентов по калорийности введите \"-{SearchOnCaloricContentRange}\"");
            builder.AppendLine($"Для выхода введите \"-{Exit}\"");

            return builder.ToString();
        }
    }
}
