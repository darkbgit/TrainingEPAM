﻿using System;
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

        private const int MAX_SENTENCE_ELEMENTS = 1000;

        public Sentence(IEnumerable<ISentenceElement> elements)
        {
            if (elements.Count() > MAX_SENTENCE_ELEMENTS)
            {
                throw new ApplicationException("Sentence too big");
            }
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
