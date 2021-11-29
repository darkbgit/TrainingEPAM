using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task2.Core.TextObjectModel.Interfaces;


namespace Task2.Core.TextObjectModel
{
    public class Sentence : ISentence
    {
        private readonly IList<ISentenceElement> _elements;

        public Sentence(IList<ISentenceElement> elements)
        {
            _elements = elements;
        }

        public int WordsCount => _elements.Count(e => e is Word);

        public override string ToString()
        {
            const char SPACE_CHAR = ' ';

            var builder = new StringBuilder();

            foreach (var element in _elements)
            {
                if (element is not PunctuationMark)
                {
                    builder.Append(SPACE_CHAR);
                }

                builder.Append(element.Writing);
            }

            return builder.ToString().TrimStart();
        }

        public IEnumerator<ISentenceElement> GetEnumerator()
        {
            return ((IEnumerable<ISentenceElement>)_elements).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_elements).GetEnumerator();
        }
    }
}
