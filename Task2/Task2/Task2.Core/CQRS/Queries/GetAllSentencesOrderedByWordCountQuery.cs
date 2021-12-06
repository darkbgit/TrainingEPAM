using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.Model;
using Task2.Core.Model.Interfaces;

namespace Task2.Core.CQRS.Queries
{
    public class GetAllSentencesOrderedByWordCountQuery
    {
        private readonly IText _text;

        public GetAllSentencesOrderedByWordCountQuery(IText text)
        {
            _text = text;
        }

        public IText Execute()
        {
            return new Text(_text.OrderBy(s => s.WordsCount));
        }
    }
}
