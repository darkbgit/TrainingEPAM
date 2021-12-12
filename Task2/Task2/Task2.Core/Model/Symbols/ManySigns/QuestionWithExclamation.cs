using Task2.Core.Model.Interfaces;

namespace Task2.Core.Model.Symbols.ManySigns
{
    public class QuestionWithExclamation : ISymbol, ISentenceElement
    {
        private const string QUESTION_WITH_EXCLAMATION_WRITING = "?!";
        public QuestionWithExclamation()
        {
            Type = SymbolType.QuestionWithExclamation;
        }


        public SymbolType Type { get; }

        public override string ToString() => QUESTION_WITH_EXCLAMATION_WRITING;
    }
}
