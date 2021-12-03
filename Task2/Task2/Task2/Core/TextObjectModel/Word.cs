using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task2.Core.TextObjectModel.Interfaces;
using Task2.Core.TextObjectModel.Symbols;

namespace Task2.Core.TextObjectModel
{
    public class Word : IEnumerable<ISymbol>, ISentenceElement
    {
        private readonly List<ISymbol> _symbols;

        public Word(IEnumerable<ISymbol> symbols)
        {
            _symbols = symbols.ToList();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var symbol in _symbols)
            {
                builder.Append(symbol.ToString());
            }

            return builder.ToString();
        }


        public IEnumerator<ISymbol> GetEnumerator()
        {
            return ((IEnumerable<ISymbol>)_symbols).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_symbols).GetEnumerator();
        }
    }
}
