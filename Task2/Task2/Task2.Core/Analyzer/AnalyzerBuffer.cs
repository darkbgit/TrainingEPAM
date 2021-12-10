using System;
using System.Collections;
using System.Collections.Generic;
using Task2.Core.Model;
using Task2.Core.Model.Interfaces;

namespace Task2.Core.Analyzer
{
    internal class AnalyzerBuffer
    {
        internal AnalyzerBuffer()
        {
            Symbols = new CollectionWithMax<ISymbol>(Params.MaxSymbolsInWord);
            SentenceElements = new CollectionWithMax<ISentenceElement>(Params.MaxElementsInSentence);
            Sentences = new List<ISentence>();
        }

        internal CollectionWithMax<ISymbol> Symbols { get; set; }

        internal CollectionWithMax<ISentenceElement> SentenceElements { get; set; }

        internal ICollection<ISentence> Sentences { get; set; }
    }
}
