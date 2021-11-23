using Chef.Cook.Units.Interfaces;
using System;

namespace Chef.Cook.Units
{
    public class Gram : IWeight
    {
        private const double GRAM_TO_GRAM = 1.0;

        private const string GRAM_NAME = "г";

        public double ToBaseUnit() => GRAM_TO_GRAM;

        public override string ToString() => GRAM_NAME;
    }
}
