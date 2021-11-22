using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chef.Cook.Units.Interfaces;

namespace Chef.Cook.Units
{
    public class Tablespoon : IVolume
    {
        private const double TABLESPOON_TO_MILLILITER = 15.0;

        private const string TABLESPOON_NAME = "столовая ложка";

        public delegate double ConvertToGram();



        public double ToGram(ConvertToGram convert)
        {

            return convert() * TABLESPOON_TO_MILLILITER;
        }

        public override string ToString() => TABLESPOON_NAME;
    }
}
