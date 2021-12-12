using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Task2.Core.Model.Interfaces;

namespace Task2.Core.Model
{
    public class Text : IText
    {
        private readonly List<ISentence> _sentences;

        public Text(IEnumerable<ISentence> sentences)
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
