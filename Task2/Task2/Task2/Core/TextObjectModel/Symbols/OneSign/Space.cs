using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.TextObjectModel.Interfaces;

namespace Task2.Core.TextObjectModel.Symbols.OneSign
{
    public class Space : ISymbol
    {

        public Space()
        {
            Type = SymbolType.Space;
        }

        public SymbolType Type { get; }

        public string Writing()
        {
            throw new NotImplementedException();
        }
    }
}
