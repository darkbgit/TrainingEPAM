// See https://aka.ms/new-console-template for more information
using Chef.Cooking;
using Chef.Ingredients;
using System.Collections.ObjectModel;

var saladIngredients = new Collection<Vegetable>();

saladIngredients.Add(new Lettuce(300));
saladIngredients.Add(new Cucumber(150));
saladIngredients.Add(new Tomato(200));


//var salad = new Cook(saladIngredients);

//salad.MakeSalad();

Console.WriteLine(saladIngredients.ToConsoleStr());

bool breakFlag = true;

while (breakFlag)
{
    Console.WriteLine("Для сортировки по свойству введите \"-s Свойство\"");
    Console.WriteLine("Свойства \"-k100\" - ККалорий в 100 грамм продукта");
    Console.WriteLine("Свойства \"-k\" - ККалорий в продукте");
    Console.WriteLine("Свойства \"-w\" - вес продукта");
    Console.WriteLine("Для поиска ингредиентов по калорийности введите \"-с\"");
    Console.WriteLine("Для выхода введите \"exit\"");
    var input = Console.ReadLine();

    if (input?.Length > 1)
    {
        switch (input[..2])
        {
            case "-s":
                Console.WriteLine(GetSortedIngredients(input));
                break;
            case "-c":
                (int bottom, int top) = GetCaloricContentRange();
                Console.WriteLine(GetIngredientsForCaloricContentRange(bottom, top));
                break;
            case "ex":
                if (input.Trim() == "exit")
                {
                    breakFlag = !breakFlag;
                }
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

string GetSortedIngredients(string input)
{
    if (input.Length > 4)
    {
        switch (input[3..].Trim())
        {
            case "-k100":
                var k100Sort = saladIngredients
                    .OrderBy(i => i.CaloricContentPer100Gram)
                    .ToList();
                return k100Sort.ToConsoleStr();
            case "-k":
                var kSort = saladIngredients
                    .OrderBy(i => i.CaloricContent)
                    .ToList();
                return kSort.ToConsoleStr();

            case "-w":
                var wSort = saladIngredients
                    .OrderBy(i => i.Weight)
                    .ToList();
                return wSort.ToConsoleStr();
            default:
                return "Неправильный параметр сортировки";
        }
    }
    else
    {
        return "Неправильный параметр сортировки";
    }
}

(int bottom, int top) GetCaloricContentRange()
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

string GetIngredientsForCaloricContentRange(int bottom, int top)
{
    return saladIngredients
        .Where(i => i.CaloricContent >= bottom && i.CaloricContent <= top)
        .OrderBy(i => i.CaloricContent)
        .ToList().ToConsoleStr();
}
