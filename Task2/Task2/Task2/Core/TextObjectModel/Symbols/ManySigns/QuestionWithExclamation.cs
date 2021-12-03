using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.TextObjectModel.Interfaces;

namespace Task2.Core.TextObjectModel.Symbols.ManySigns
{
    internal class QuestionWithExclamation : ISymbol
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
