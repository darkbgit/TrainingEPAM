using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.TextParts;
using Task2.Core.TextParts.Interfaces;

namespace Task2.Core
{
    public class AnalyzedText : IAnalyzedText
    {
        private readonly List<ISentence> _sentences;

        public AnalyzedText(IEnumerable<ISentence> sentences)
        {
            _sentences = sentences.ToList();
        }

        public IEnumerator<ISentence> GetEnumerator()
        {
            return ((IEnumerable<ISentence>)_sentences).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_sentences).GetEnumerator();
        }
    }
}
