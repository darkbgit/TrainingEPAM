using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.Model.Interfaces;

namespace Task2.Core.Model.Symbols
{
    internal class BeginSymbol : ISymbol
    {
        public BeginSymbol()
        {
            Type = SymbolType.Begin;
        }
        public SymbolType Type { get; }
    }
}
