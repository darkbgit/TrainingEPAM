using Chef.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Cooking
{
    public static class OutputToConsole
    {
        public static string ToConsoleStr(this IEnumerable<Vegetable> ingredients)
        {
            const int columnCount = 4;
            const int nameWidth = 20;
            const int caloricContentPer100GramWidth = 20;
            const int caloricContentWidth = 10;
            const int weightWidth = 10;
            const int totalWidth = nameWidth + caloricContentPer100GramWidth + caloricContentWidth + weightWidth + columnCount - 1;


            var builder = new StringBuilder();
            builder.Append('_', totalWidth);
            builder.Append(Environment.NewLine);
            builder.Append($"{"Ингредиент",nameWidth}");
            builder.Append('|');
            builder.Append($"{"ККал в 100 грамм",caloricContentPer100GramWidth}");
            builder.Append('|');
            builder.Append($"{"ККал общее",caloricContentWidth}");
            builder.Append('|');
            builder.Append($"{"Вес",weightWidth}");
            builder.Append(Environment.NewLine);
            builder.Append('_', totalWidth);
            builder.Append(Environment.NewLine);

            var vegetables = ingredients.ToList();
            foreach (var ingredient in vegetables)
            {
                builder.Append($"{ingredient.Name,nameWidth}");
                builder.Append('|');
                builder.Append($"{ingredient.CaloricContentPer100Gram,caloricContentPer100GramWidth}");
                builder.Append('|');
                builder.Append($"{ingredient.CaloricContent,caloricContentWidth}");
                builder.Append('|');
                builder.Append($"{ingredient.Weight,weightWidth}");
                builder.Append(Environment.NewLine);
            }
            builder.Append('_', totalWidth);
            builder.Append(Environment.NewLine);
            builder.Append("Общая калорийность ");
            builder.Append(vegetables.Sum(i => i.CaloricContent));
            builder.Append(" ККал");
            builder.Append(Environment.NewLine);
            builder.Append('_', totalWidth);

            return builder.ToString();
        }
    }
}
