using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task2.Core.TextObjectModel.Interfaces;
using Task2.Core.TextObjectModel.Symbols;
using Task2.Core.TextObjectModel.Symbols.OneSign;


namespace Task2.Core.TextObjectModel
{
    public class Sentence : ISentence
    {
        private readonly IList<ISentenceElement> _elements;

        public Sentence(IEnumerable<ISentenceElement> elements)
        {
            _elements = elements.ToList();
        }

        public int WordsCount => _elements.Count(e => e is Word);

        public override string ToString()
        {
            const char SPACE_CHAR = ' ';

            var builder = new StringBuilder();

            for (var i = 0; i < _elements.Count; i++)
            {
                var element = _elements[i];
                if (i > 0 && element is not IPunctuation)
                {
                    builder.Append(SPACE_CHAR);
                }

                builder.Append(element);
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
