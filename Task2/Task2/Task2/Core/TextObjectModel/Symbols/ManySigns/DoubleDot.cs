using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Core.TextObjectModel.Symbols.ManySigns
{
    internal class DoubleDot : ISymbol
    {

        public DoubleDot()
        {
            Type = SymbolType.DoubleDot;
        }


        public SymbolType Type { get; }


    }
}
