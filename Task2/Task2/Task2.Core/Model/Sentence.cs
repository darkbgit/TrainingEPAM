using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task2.Core.Model.Interfaces;

namespace Task2.Core.Model
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
            var builder = new StringBuilder();

            foreach (var element in _elements)
            {
                builder.Append(element.ToString());
            }

            return builder.ToString();
        }

        public void RemoveAt(int index)
        {
            _elements.RemoveAt(index);
        }

        public void AddRangeAt(int index, IEnumerable<ISentenceElement> elements)
        {
            var sentenceElements = elements.ToList();

            for (int i = 0; i < sentenceElements.Count; i++)
            {
                _elements.Insert(index + i, sentenceElements[i]);
            }
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
