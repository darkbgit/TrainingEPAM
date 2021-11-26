using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Output
{
    public enum CommandLineCommand
    {
        Base,
        PrintData,
        SortOnCaloricContent,
        SortOnIngredientName,
        SearchOnCaloricContentRange,
        Exit,
        UndefinedCommand
    }
}

