using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task2.Core.TextObjectModel.Interfaces;

namespace Task2.Core.TextObjectModel
{
    public class Text : IText
    {
        private readonly List<ISentence> _sentences;

        public Text(IEnumerable<ISentence> sentences)
        {
            _sentences = sentences.ToList();
        }

        public override string ToString()
        {
            const char SPACE_CHAR = ' ';

            var builder = new StringBuilder();
            foreach (var sentence in _sentences)
            {
                builder.Append(sentence);
                builder.Append(SPACE_CHAR);
            }

            return builder.ToString();
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
