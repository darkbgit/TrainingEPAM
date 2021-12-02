using System;
using System.Collections.Generic;
using System.Linq;
using Task2.Core.TextObjectModel;
using Task2.Core.TextObjectModel.Interfaces;
using Task2.Core.TextObjectModel.Symbols;
using Task2.Core.TextObjectModel.Symbols.ManySigns;
using Task2.Core.TextObjectModel.Symbols.OneSign;

namespace Task2.Core.StateMachine
{

    internal delegate void SymbolChangeDelegate(ISymbol nextSymbol,
        ref ICollection<ISymbol> symbols, ref ICollection<ISentenceElement> elements,
        ref ICollection<ISentence> sentences);

    internal class StateMachine : IStateMachine
    {
        private readonly Dictionary<StateTransition, SymbolChangeDelegate> _transitions;

        private SymbolType _currentState;

        internal StateMachine()
        {
            _currentState = SymbolType.Begin;
            _transitions = new Dictionary<StateTransition, SymbolChangeDelegate>
            {
                {new StateTransition(SymbolType.Begin, SymbolType.LetterOrDigit), AddSymbol},
                {new StateTransition(SymbolType.Begin, SymbolType.PunctuationMark), AddSymbol},
                {new StateTransition(SymbolType.Begin, SymbolType.Dot), AddSymbol}, //TODO
                {new StateTransition(SymbolType.Begin, SymbolType.Space), SkipSymbol},

                {new StateTransition(SymbolType.LetterOrDigit, SymbolType.LetterOrDigit), AddSymbol},
                {new StateTransition(SymbolType.LetterOrDigit, SymbolType.PunctuationMark), MakeWordAndAddSymbol},
                {new StateTransition(SymbolType.LetterOrDigit, SymbolType.Dot), MakeWordAndAddSymbol},
                {new StateTransition(SymbolType.LetterOrDigit, SymbolType.Question), MakeWordAndAddSymbol},
                {new StateTransition(SymbolType.LetterOrDigit, SymbolType.Exclamation), MakeWordAndAddSymbol},
                {new StateTransition(SymbolType.LetterOrDigit, SymbolType.Space), MakeWordAndAddSymbol},


                {new StateTransition(SymbolType.PunctuationMark, SymbolType.LetterOrDigit), MakePunctuationAndAddSymbol},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.PunctuationMark), MakePunctuationAndAddSymbol},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.Dot), MakePunctuationAndAddSymbol},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.Question), MakePunctuationAndAddSymbol},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.Exclamation), MakePunctuationAndAddSymbol},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.Space), MakePunctuationAndAddSymbol},

                {new StateTransition(SymbolType.Dot, SymbolType.LetterOrDigit), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Dot, SymbolType.PunctuationMark), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Dot, SymbolType.Dot), MakeDoubleDot},
                {new StateTransition(SymbolType.Dot, SymbolType.Space), MakePunctuationAndMakeSentenceAndAddSymbol},

                {new StateTransition(SymbolType.DoubleDot, SymbolType.Dot), MakeEllipsis},

                {new StateTransition(SymbolType.Ellipsis, SymbolType.LetterOrDigit), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Dot, SymbolType.PunctuationMark), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Dot, SymbolType.Dot), MakeDoubleDot},
                {new StateTransition(SymbolType.Dot, SymbolType.Space), MakePunctuationAndMakeSentenceAndAddSymbol},

                {new StateTransition(SymbolType.Question, SymbolType.LetterOrDigit), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Question, SymbolType.PunctuationMark), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Question, SymbolType.Dot), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Question, SymbolType.Exclamation), MakePunctuationAndMakeSentenceAndAddSymbol}, // TODO
                {new StateTransition(SymbolType.Question, SymbolType.Space), MakePunctuationAndMakeSentenceAndAddSymbol},

                {new StateTransition(SymbolType.Exclamation, SymbolType.LetterOrDigit), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Exclamation, SymbolType.PunctuationMark), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Exclamation, SymbolType.Space), MakePunctuationAndMakeSentenceAndAddSymbol},


                {new StateTransition(SymbolType.Space, SymbolType.LetterOrDigit), MakeSpaceAndAddSymbol},
                {new StateTransition(SymbolType.Space, SymbolType.PunctuationMark), MakeSpaceAndAddSymbol},
                {new StateTransition(SymbolType.Space, SymbolType.Dot), MakeSpaceAndAddSymbol},
                {new StateTransition(SymbolType.Space, SymbolType.Space), SkipSymbol},


                {new StateTransition(SymbolType.LetterOrDigit, SymbolType.End), EndWithLetterOrDigit},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.End), EndWithPunctuation},
                {new StateTransition(SymbolType.Dot, SymbolType.End), EndWithPunctuationEndSentence},
                {new StateTransition(SymbolType.Question, SymbolType.End), EndWithPunctuationEndSentence},
                {new StateTransition(SymbolType.Exclamation, SymbolType.End), EndWithPunctuationEndSentence},
                {new StateTransition(SymbolType.Space, SymbolType.End), EndWithSpace}
            };
        }

        private SymbolChangeDelegate GetNext(SymbolType nextSymbol)
        {
            var transition = new StateTransition(_currentState, nextSymbol);

            if (!_transitions.TryGetValue(transition, out var command))
                throw new ArgumentException("Invalid transition: " + _currentState + " -> " + nextSymbol);

            return command;
        }

        public SymbolChangeDelegate MoveNext(SymbolType nextSymbol)
        {
            var command = GetNext(nextSymbol);
            _currentState = nextSymbol;

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

        private void MakeSpaceAndAddSymbol(ISymbol nextSymbol,
            ref ICollection<ISymbol> symbols, ref ICollection<ISentenceElement> elements,
            ref ICollection<ISentence> sentences)
        {
            elements.Add(new Space());
            symbols.Clear();
            symbols.Add(nextSymbol);
        }

        private void MakeWordAndAddSymbol(ISymbol nextSymbol,
            ref ICollection<ISymbol> symbols, ref ICollection<ISentenceElement> elements,
            ref ICollection<ISentence> sentences)
        {
            elements.Add(new Word(symbols));
            symbols.Clear();
            symbols.Add(nextSymbol);
        }


        private void MakeDoubleDot(ISymbol nextSymbol,
            ref ICollection<ISymbol> symbols, ref ICollection<ISentenceElement> elements,
            ref ICollection<ISentence> sentences)
        {
            symbols.Clear();
            symbols.Add(new DoubleDot());
        }


        private void MakeEllipsis(ISymbol nextSymbol,
            ref ICollection<ISymbol> symbols, ref ICollection<ISentenceElement> elements,
            ref ICollection<ISentence> sentences)
        {
            symbols.Clear();
            symbols.Add(new Ellipsis());
        }

        private void MakePunctuationAndAddSymbol(ISymbol nextSymbol,
            ref ICollection<ISymbol> symbols, ref ICollection<ISentenceElement> elements,
            ref ICollection<ISentence> sentences)
        {
            elements.Add(new PunctuationMark(symbols.Last()));
            symbols.Clear();
            symbols.Add(nextSymbol);
        }


        private void MakePunctuationAndMakeSentenceAndAddSymbol(ISymbol nextSymbol,
            ref ICollection<ISymbol> symbols, ref ICollection<ISentenceElement> elements,
            ref ICollection<ISentence> sentences)
        {
            elements.Add(new PunctuationMark(symbols));
            symbols.Clear();
            sentences.Add(new Sentence(elements));
            elements.Clear();
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
            if (sentences.LastOrDefault() != null &&
                sentences.LastOrDefault().Any(e => e is EndOfSentencePunctuationMark))
            {
                sentences.Add(new Sentence(elements));
            }
            
        }

        private void EndWithPunctuationEndSentence(ISymbol nextSymbol,
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
                    case SymbolType.Dot:
                    case SymbolType.Exclamation:
                    case SymbolType.Question:
                        elements.Add(new PunctuationMark(symbols));
                        break;
                    default:
                        break;
                }
            }

            sentences.Add(new Sentence(elements));
        }

    }


}
