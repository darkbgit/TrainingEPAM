using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Chef.Cook;

namespace Chef.Output
{
    public interface IOutput
    {
        void Print(string str);

        void Print(ISalad salad);

        void PrintHelp();

        string GetUserInput();

        (int top, int bottom) GetUserCaloricContentRange();
    }
}
