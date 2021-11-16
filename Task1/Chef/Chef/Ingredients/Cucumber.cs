using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Ingredients
{
    public class Cucumber : Vegetable
    {
        public Cucumber(double weight) 
            : base(weight)
        {
            Name = "Огурец";
            CaloricContentPer100Gram = 16;
        }
    }
}
