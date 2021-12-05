using System.Collections.Generic;
using Task2.Core.Model.Interfaces;

namespace Task2.Core.Analyzer
{
    internal class AnalyzerBuffer
    {
        public AnalyzerBuffer()
        {
            Symbols = new List<ISymbol>();
            SentenceElements = new List<ISentenceElement>();
            Sentences = new List<ISentence>();
        }

        public ICollection<ISymbol> Symbols { get; set; }

        public ICollection<ISentenceElement> SentenceElements { get; set; }

        public ICollection<ISentence> Sentences { get; set; }
    }
}
