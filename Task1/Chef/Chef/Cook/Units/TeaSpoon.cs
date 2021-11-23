using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chef.Cook.Units.Interfaces;

namespace Chef.Cook.Units
{
    public class TeaSpoon : IVolume
    {
        private const double TEA_SPOON_TO_GRAM = 1.0;

        private const string TEA_SPOON_NAME = "чайна ложка";

        public double ToBaseUnit() => TEA_SPOON_TO_GRAM;

        public override string ToString() => TEA_SPOON_NAME;

    }
}
