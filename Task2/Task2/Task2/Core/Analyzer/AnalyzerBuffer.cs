using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.StateMachine;
using Task2.Core.TextObjectModel.Interfaces;
using Task2.Core.TextObjectModel.Symbols;

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
