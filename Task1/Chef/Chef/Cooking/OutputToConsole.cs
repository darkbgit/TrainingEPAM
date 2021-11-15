using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Cooking
{
    public static class OutputToConsole
    {
        public static void ToConsole(this SaladIngredients ingredients)
        {
            const int collumnCount = 4;
            const int nameWidth = 20;
            const int caloricContentPer100GrammWidth = 20;
            const int caloricContentWidth = 10;
            const int weightWidth = 10;
            const int totalWidth = nameWidth + caloricContentPer100GrammWidth + caloricContentWidth + weightWidth + collumnCount - 1;


            var builder = new StringBuilder();
            builder.Append('_', totalWidth);
            builder.Append(Environment.NewLine);
            builder.Append($"{"Ингридиент",nameWidth}");
            builder.Append('|');
            builder.Append($"{"ККал в 100 грамм",caloricContentPer100GrammWidth}");
            builder.Append('|');
            builder.Append($"{"ККал общее",caloricContentWidth}");
            builder.Append('|');
            builder.Append($"{"Вес",weightWidth}");
            builder.Append(Environment.NewLine);
            builder.Append('_', totalWidth);
            builder.Append(Environment.NewLine);

            foreach (var ingredient in ingredients)
            {
                builder.Append($"{ingredient.Name,nameWidth}");
                builder.Append('|');
                builder.Append($"{ingredient.CaloricContentPer100Gramm,caloricContentPer100GrammWidth}");
                builder.Append('|');
                builder.Append($"{ingredient.CaloricContent,caloricContentWidth}");
                builder.Append('|');
                builder.Append($"{ingredient.Weight,weightWidth}");
                builder.Append(Environment.NewLine);
            }
            builder.Append('_', totalWidth);
            Console.WriteLine(builder.ToString());
        }
    }
}
