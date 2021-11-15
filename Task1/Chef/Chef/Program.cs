// See https://aka.ms/new-console-template for more information
using Chef.Cooking;
using Chef.Ingredients;


var saladIngredients = new SaladIngredients();

saladIngredients.Add(new Lettuce(100));
saladIngredients.Add(new Cucumber(150));
saladIngredients.Add(new Tomato(200));


//var salad = new Cook(saladIngredients);

//salad.MakeSalad();

saladIngredients.ToConsole();

bool work = true;

while (work)
{
    Console.WriteLine("Для сортировки по свойству введите \"-s Свойство\"");
    Console.WriteLine("Для поиска ингридиентов по каллорийности введите \"-с\"");
    Console.WriteLine("Для выхода введите \"exit\"");
    var input = Console.ReadLine();
    if (input?.Length > 1)
    {
        switch (input[..2])
        {
            case "-s":
                var result = saladIngredients.OrderBy(i => i.Name).ToList;
                result.ToConsole();
                break;
            case "-c":
                break;
            case "ex":
                if (input.Trim() == "exit")
                {
                    work = !work;
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
