using System;
using System.Collections;
using System.Collections.Generic;
using Task2.Core.Model.Interfaces;

namespace Task2.Core.Analyzer
{
    internal class AnalyzerBuffer
    {
        private const int MAX_ELEMENTS_IN_SENTENCE = 500;

        private const int MAX_SYMBOLS_IN_WORD = 50;

        internal AnalyzerBuffer()
        {
            Symbols = new CollectionWithMax<ISymbol>(MAX_SYMBOLS_IN_WORD);
            SentenceElements = new CollectionWithMax<ISentenceElement>(MAX_ELEMENTS_IN_SENTENCE);
            Sentences = new List<ISentence>();
        }

        internal CollectionWithMax<ISymbol> Symbols { get; set; }

        internal CollectionWithMax<ISentenceElement> SentenceElements { get; set; }

        internal ICollection<ISentence> Sentences { get; set; }
    }
}
