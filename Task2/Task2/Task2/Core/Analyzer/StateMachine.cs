using System;
using System.Collections.Generic;
using System.Linq;
using Task2.Core.TextObjectModel;
using Task2.Core.TextObjectModel.Interfaces;
using Task2.Core.TextObjectModel.Symbols;
using Task2.Core.TextObjectModel.Symbols.OneSign;

namespace Task2.Core.Analyzer
{

    public delegate void SymbolChangeDelegate(ISymbol nextSymbol,
        ref ICollection<ISymbol> symbols, ref ICollection<ISentenceElement> elements,
        ref ICollection<ISentence> sentences);

    public class StateMachine
    {
        private readonly Dictionary<StateTransition, SymbolChangeDelegate> _transitions;

        public SymbolType CurrentState { get; private set; }

        public StateMachine()
        {
            CurrentState = SymbolType.Begin;
            _transitions = new Dictionary<StateTransition, SymbolChangeDelegate>
            {
                {new StateTransition(SymbolType.Begin, SymbolType.Letter), AddSymbol},
                {new StateTransition(SymbolType.Begin, SymbolType.Digit), AddSymbol},
                {new StateTransition(SymbolType.Begin, SymbolType.PunctuationMark), AddSymbol},
                {new StateTransition(SymbolType.Begin, SymbolType.Space), SkipSymbol},

                {new StateTransition(SymbolType.Digit, SymbolType.Letter), AddSymbol},
                {new StateTransition(SymbolType.Digit, SymbolType.Digit), AddSymbol},
                {new StateTransition(SymbolType.Digit, SymbolType.PunctuationMark), MakeWordAndPunctuation},
                {new StateTransition(SymbolType.Digit, SymbolType.Space), MakeWord},

                {new StateTransition(SymbolType.Letter, SymbolType.Letter), AddSymbol},
                {new StateTransition(SymbolType.Letter, SymbolType.Digit), AddSymbol},
                {new StateTransition(SymbolType.Letter, SymbolType.PunctuationMark), MakeWordAndPunctuation},
                {new StateTransition(SymbolType.Letter, SymbolType.Space), MakeWord},

                {new StateTransition(SymbolType.PunctuationMark, SymbolType.Letter), MakePunctuationAndContinue},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.Digit), MakePunctuationAndContinue},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.PunctuationMark), MakePunctuationAndContinue},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.Space), MakePunctuation},



                {new StateTransition(SymbolType.Space, SymbolType.Letter), AddSymbol},
                {new StateTransition(SymbolType.Space, SymbolType.Digit), AddSymbol},
                {new StateTransition(SymbolType.Space, SymbolType.PunctuationMark), AddSymbol},
                {new StateTransition(SymbolType.Space, SymbolType.Space), SkipSymbol},


                {new StateTransition(SymbolType.Letter, SymbolType.End), EndWithLetterOrDigit},
                {new StateTransition(SymbolType.Digit, SymbolType.End), EndWithLetterOrDigit},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.End), EndWithPunctuation},
                {new StateTransition(SymbolType.Space, SymbolType.End), EndWithSpace}

            };
        }

        private SymbolChangeDelegate GetNext(SymbolType nextSymbol)
        {
            var transition = new StateTransition(CurrentState, nextSymbol);

            if (!_transitions.TryGetValue(transition, out var command))
                throw new ArgumentException("Invalid transition: " + CurrentState + " -> " + nextSymbol);

            return command;
        }

        public SymbolChangeDelegate MoveNext(SymbolType nextSymbol)
        {
            SymbolChangeDelegate command;
            try
            {
                command = GetNext(nextSymbol);
                CurrentState = nextSymbol;
            }
            catch (ArgumentException)
            {
                command = null;
            }

            return command;
        }

        

        private void AddSymbol(ISymbol nextSymbol,
            ref ICollection<ISymbol> symbols, ref ICollection<ISentenceElement> elements,
            ref ICollection<ISentence> sentences)
        {
            symbols.Add(nextSymbol);
        }

        private void SkipSymbol(ISymbol nextSymbol,
            ref ICollection<ISymbol> symbols, ref ICollection<ISentenceElement> elements,
            ref ICollection<ISentence> sentences)
        {

        }

        private void MakeWord(ISymbol nextSymbol,
            ref ICollection<ISymbol> symbols, ref ICollection<ISentenceElement> elements,
            ref ICollection<ISentence> sentences)
        {
            elements.Add(new Word(symbols));
            symbols.Clear();
        }

        private void MakeWordAndPunctuation(ISymbol nextSymbol,
            ref ICollection<ISymbol> symbols, ref ICollection<ISentenceElement> elements,
            ref ICollection<ISentence> sentences)
        {
            elements.Add(new Word(symbols));
            symbols.Clear();
            symbols.Add(nextSymbol);
        }

        private void MakePunctuation(ISymbol nextSymbol,
            ref ICollection<ISymbol> symbols, ref ICollection<ISentenceElement> elements,
            ref ICollection<ISentence> sentences)
        {
            elements.Add(new PunctuationMark(symbols.Last()));
            symbols.Clear();
        }

        private void MakePunctuationAndContinue(ISymbol nextSymbol,
            ref ICollection<ISymbol> symbols, ref ICollection<ISentenceElement> elements,
            ref ICollection<ISentence> sentences)
        {
            elements.Add(new PunctuationMark(symbols.Last()));
            symbols.Clear();
            symbols.Add(nextSymbol);
        }

        private void EndWithLetterOrDigit(ISymbol nextSymbol,
            ref ICollection<ISymbol> symbols, ref ICollection<ISentenceElement> elements,
            ref ICollection<ISentence> sentences)
        {
            elements.Add(new Word(symbols));
            symbols.Clear();
            
            sentences.Add(new Sentence(elements));
        }

        private void EndWithPunctuation(ISymbol nextSymbol,
            ref ICollection<ISymbol> symbols, ref ICollection<ISentenceElement> elements,
            ref ICollection<ISentence> sentences)
        {
            elements.Add(new Word(symbols));
            symbols.Clear();
            elements.Add(new PunctuationMark(nextSymbol));
            sentences.Add(new Sentence(elements));
        }

        private void EndWithSpace(ISymbol nextSymbol,
            ref ICollection<ISymbol> symbols, ref ICollection<ISentenceElement> elements,
            ref ICollection<ISentence> sentences)
        {
            if (symbols.Any())
            {
                switch (symbols.LastOrDefault().Type)
                {
                    case SymbolType.Digit:
                    case SymbolType.Letter:
                        elements.Add(new Word(symbols));
                        break;
                    case SymbolType.PunctuationMark:
                        elements.Add(new PunctuationMark(symbols.Last()));
                        break;
                    default:
                        break;

                }
            }

            sentences.Add(new Sentence(elements));
        }

    }


}
