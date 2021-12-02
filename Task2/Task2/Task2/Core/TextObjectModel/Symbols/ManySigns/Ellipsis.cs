using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.TextObjectModel.Interfaces;

namespace Task2.Core.TextObjectModel.Symbols.ManySigns
{
    internal class Ellipsis : ISymbol
    {
        private const string ELLIPSIS_WRITING = "...";
        public Ellipsis()
        {
            Type = SymbolType.Ellipsis;
        }


        public SymbolType Type { get; }

        public override string ToString() => ELLIPSIS_WRITING;
    }
}
