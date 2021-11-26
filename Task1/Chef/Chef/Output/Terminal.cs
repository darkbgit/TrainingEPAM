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

        private static string GenerateHelpString()

        {
            var builder = new StringBuilder();
            builder.AppendLine("usage: chef");
            var firstLength = builder.Length;
            builder.Append(' ', firstLength);
            builder.AppendLine($"[{CommandLineArguments.PrintInitialData}] - вывод данных");
            builder.Append(' ', firstLength);
            builder.AppendLine($"[{CommandLineArguments.SortOnCaloricContent}] - сортировка по калорийности");
            builder.Append(' ', firstLength);
            builder.AppendLine($"[{CommandLineArguments.SortOnIngredientName}] - сортировка по названию ингредиента");
            builder.Append(' ', firstLength);
            builder.AppendLine($"[{CommandLineArguments.SearchOnCaloricContentRange} <min> <max>] - поиск ингредиентов в диапазоне калорийности");

            return builder.ToString();
        }
    }
}
