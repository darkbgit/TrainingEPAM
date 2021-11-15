using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Ingredients
{
    public class Lettuce : Vegetable
    {
        public Lettuce(double weight) 
            : base(weight)
        {
            Name = "Салат-латук";
            CaloricContentPer100Gramm = 15;
        }
    }
}
