// See https://aka.ms/new-console-template for more information
using Chef.Cook;
using Chef.Cook.Ingredients;

const string Sort = "s";
const string CaloricContent100 = "k100";
const string CaloricContent = "k";
const string Weight = "w";
const string SearchCaloricContentRange = "c";
const string Exit = "e";


var salad = new Salad<Vegetable>
{
    new Lettuce(300),
    new Cucumber(150),
    new Tomato(200)

};


Console.WriteLine(salad.ToConsoleStr());

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
                Console.WriteLine(GetSortedIngredients(input));
                break;
            case SearchCaloricContentRange:
                (int bottom, int top) = GetCaloricContentRange();
                Console.WriteLine(GetIngredientsForCaloricContentRange(bottom, top));
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

string GetSortedIngredients(string[] input)
{
    if (input.Length == 2)
    {
        switch (input[1])
        {
            case CaloricContent100:
                var k100Sort = new Salad<Vegetable>(salad
                    .OrderBy(i => i.CaloricContentPer100Gram));
                return k100Sort.ToConsoleStr();
            case CaloricContent:
                var kSort = new Salad<Vegetable>(salad
                    .OrderBy(i => i.CaloricContent));
                return kSort.ToConsoleStr();
            case Weight:
                var wSort = new Salad<Vegetable>(salad
                    .OrderBy(i => i.Weight));
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
    var result = new Salad<Vegetable>(salad
        .Where(i => i.CaloricContent >= bottom && i.CaloricContent <= top)
        .OrderBy(i => i.CaloricContent));
    return result.ToConsoleStr();
}
