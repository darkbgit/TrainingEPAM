using System.Collections.Generic;
using System.Linq;
using Task2.Core.Model.Interfaces;

namespace Task2.Core.Tasks.Queries
{
    public class GetAllSentencesOrderedByWordCountQuery
    {
        private readonly IText _text;

        public GetAllSentencesOrderedByWordCountQuery(IText text)
        {
            _text = text;
        }

        public IEnumerable<ISentence> Execute()
        {
            return _text.OrderBy(s => s.WordsCount);
        }
    }
}
