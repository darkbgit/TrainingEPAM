using Task2.Core.Model.Interfaces;

namespace Task2.Core.Model.Symbols.OneSign
{
    public class Question : Symbol, ISymbol, ISentenceElement
    {
        private const char QUESTION_CHAR = '?';
        public Question()
            : base(QUESTION_CHAR, SymbolType.Question)
        {

        }

        public override string ToString() => _symbol.ToString();
    }
}
