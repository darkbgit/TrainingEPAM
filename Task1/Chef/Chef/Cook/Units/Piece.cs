using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chef.Cook.Units.Interfaces;

namespace Chef.Cook.Units
{
    public class Piece : IPiece
    {
        private readonly string _unitName;
        private readonly double _weight;

        public Piece(string unitName)
        {
            _unitName = unitName;
            _weight = 0;
        }

        public Piece(string unitName, double weightInGrams)
        {
            _unitName = unitName;
            _weight = weightInGrams;
        }

        public double ToGram() => _weight;

        public override string ToString() => _unitName;

    }
}
