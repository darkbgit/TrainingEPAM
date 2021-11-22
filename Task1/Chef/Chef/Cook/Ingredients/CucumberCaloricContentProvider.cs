﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chef.Cook.Ingredients.Base;
using Chef.Cook.Units;
using Chef.Cook.Units.Interfaces;

namespace Chef.Cook.Ingredients
{
    public class CucumberCaloricContentProvider : ICaloricContentProvider
    {
        private const double CUCUMBER_CALORIC_CONTENT_PER_GRAM = 0.16;

        private readonly IUnit _unit;

        public CucumberCaloricContentProvider(IWeight unit)
        {
            _unit = unit;
        }

        public double GetCaloricContent()
        {
            return _unit.ToGram() * CUCUMBER_CALORIC_CONTENT_PER_GRAM;
        }
    }
}
