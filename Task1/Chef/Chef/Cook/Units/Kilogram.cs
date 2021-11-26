using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chef.Cook.Units.Interfaces;

namespace Chef.Cook.Units
{
    internal class Kilogram : IWeight
    {
        private const double KILOGRAM_TO_GRAM = 1000.0;

        private const string KILOGRAM_NAME = "кг";

        public double ToBaseUnit() => KILOGRAM_TO_GRAM;

        public override string ToString() => KILOGRAM_NAME;
    }
}
