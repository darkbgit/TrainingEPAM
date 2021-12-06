using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task2.Core.Model.Interfaces;
using Task2.Core.Model.Symbols;

namespace Task2.Core.Model
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
                builder.Append(symbol);
            }

            return builder.ToString();
        }

        public int Length() => _symbols.Count;

        public IEnumerator<ISymbol> GetEnumerator()
        {
            return ((IEnumerable<ISymbol>)_symbols).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_symbols).GetEnumerator();
        }

        public override int GetHashCode()
        {
            return 17 + 31 * _symbols.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is Word other && _symbols == other._symbols;
        }
    }
}
