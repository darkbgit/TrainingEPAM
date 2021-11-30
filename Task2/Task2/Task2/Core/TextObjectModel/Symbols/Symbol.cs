using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Core.TextObjectModel.Symbols
{
    public abstract class Symbol
    {
        protected readonly char _symbol;
        protected Symbol(char symbol, SymbolType type)
        {
            _symbol = symbol;
            Type = type;
        }

        public SymbolType Type { get; }

        //public new string ToString() => _symbol.ToString();

        public string Writing() => _symbol.ToString();
    }
}
