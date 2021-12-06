using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.Model;
using Task2.Core.Model.Interfaces;
using Task2.Core.Model.Symbols.ManySigns;
using Task2.Core.Model.Symbols.OneSign;

namespace Task2.Core.CQRS.Queries
{
    public class GetAllWordsFromQuestionsQuery
    {
        private readonly IText _text;

        public GetAllWordsFromQuestionsQuery(IText text)
        {
            _text = text;
        }

        public IEnumerable<Word> Execute(int wordLength)
        {
            var result = _text.Where(s => s.LastOrDefault() is Question or QuestionWithExclamation)
                .SelectMany(s => s.OfType<Word>())
                .Where(w => w.Length() == wordLength)
                .Distinct()
                .ToList();
            return result;
        }
    }
}
