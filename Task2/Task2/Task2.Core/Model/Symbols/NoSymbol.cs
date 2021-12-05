using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.Model.Interfaces;

namespace Task2.Core.Model.Symbols
{
    public class NoSymbol : ISymbol
    {
        public NoSymbol()
        {
            Type = SymbolType.NoSymbol;
        }
        public SymbolType Type { get; }
    }
}
