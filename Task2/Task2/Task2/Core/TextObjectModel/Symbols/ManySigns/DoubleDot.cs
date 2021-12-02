using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Core.TextObjectModel.Symbols.ManySigns
{
    internal class DoubleDot : ISymbol
    {
        private const string ELLIPSIS_WRITING = "...";
        public DoubleDot()
        {
            Type = SymbolType.Ellipsis;
        }


        public SymbolType Type { get; }

        public override string ToString() => ELLIPSIS_WRITING;
    }
}
