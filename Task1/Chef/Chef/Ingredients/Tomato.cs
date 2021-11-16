using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Ingredients
{
    public class Tomato : Vegetable
    {
        public Tomato(double weight) : base(weight)
        {
            Name = "Помидор";
            CaloricContentPer100Gram = 19.9;
        }
    }
}
