using System;
using System.Collections.Generic;
using System.Linq;
using Task2.Core.TextObjectModel;
using Task2.Core.TextObjectModel.Interfaces;
using Task2.Core.TextObjectModel.Symbols;
using Task2.Core.TextObjectModel.Symbols.OneSign;

namespace Task2.Core.StateMachine
{

    public delegate void SymbolChangeDelegate(ISymbol nextSymbol,
        ref ICollection<ISymbol> symbols, ref ICollection<ISentenceElement> elements,
        ref ICollection<ISentence> sentences);

    public class StateMachine : IStateMachine
    {
        private readonly Dictionary<StateTransition, SymbolChangeDelegate> _transitions;

        private SymbolType _currentState;

        public StateMachine()
        {
            _currentState = SymbolType.Begin;
            _transitions = new Dictionary<StateTransition, SymbolChangeDelegate>
            {
                {new StateTransition(SymbolType.Begin, SymbolType.Letter), AddSymbol},
                {new StateTransition(SymbolType.Begin, SymbolType.Digit), AddSymbol},
                {new StateTransition(SymbolType.Begin, SymbolType.PunctuationMark), SkipSymbol},
                {new StateTransition(SymbolType.Begin, SymbolType.Dot), AddSymbol}, //TODO
                {new StateTransition(SymbolType.Begin, SymbolType.Question), SkipSymbol},
                {new StateTransition(SymbolType.Begin, SymbolType.Exclamation), SkipSymbol},
                {new StateTransition(SymbolType.Begin, SymbolType.Space), SkipSymbol},

                {new StateTransition(SymbolType.Digit, SymbolType.Letter), AddSymbol},
                {new StateTransition(SymbolType.Digit, SymbolType.Digit), AddSymbol},
                {new StateTransition(SymbolType.Digit, SymbolType.PunctuationMark), MakeWordAndAddSymbol},
                {new StateTransition(SymbolType.Digit, SymbolType.Dot), MakeWordAndAddSymbol},
                {new StateTransition(SymbolType.Digit, SymbolType.Question), MakeWordAndAddSymbol},
                {new StateTransition(SymbolType.Digit, SymbolType.Exclamation), MakeWordAndAddSymbol},
                {new StateTransition(SymbolType.Digit, SymbolType.Space), MakeWordAndAddSymbol},

                {new StateTransition(SymbolType.Letter, SymbolType.Letter), AddSymbol},
                {new StateTransition(SymbolType.Letter, SymbolType.Digit), AddSymbol},
                {new StateTransition(SymbolType.Letter, SymbolType.PunctuationMark), MakeWordAndAddSymbol},
                {new StateTransition(SymbolType.Letter, SymbolType.Dot), MakeWordAndAddSymbol},
                {new StateTransition(SymbolType.Letter, SymbolType.Question), MakeWordAndAddSymbol},
                {new StateTransition(SymbolType.Letter, SymbolType.Exclamation), MakeWordAndAddSymbol},
                {new StateTransition(SymbolType.Letter, SymbolType.Space), MakeWordAndAddSymbol},

                {new StateTransition(SymbolType.PunctuationMark, SymbolType.Letter), MakePunctuationAndAddSymbol},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.Digit), MakePunctuationAndAddSymbol},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.PunctuationMark), MakePunctuationAndAddSymbol},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.Dot), MakePunctuationAndAddSymbol},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.Question), MakePunctuationAndAddSymbol},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.Exclamation), MakePunctuationAndAddSymbol},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.Space), MakePunctuationAndAddSymbol},


                {new StateTransition(SymbolType.Dot, SymbolType.Letter), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Dot, SymbolType.Digit), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Dot, SymbolType.PunctuationMark), MakePunctuationAndMakeSentenceAndAddSymbol}, //TODO comma
                {new StateTransition(SymbolType.Dot, SymbolType.Dot), MakeWordAndPunctuationEndSentence}, //TODO
                {new StateTransition(SymbolType.Dot, SymbolType.Question), MakePunctuationAndMakeSentenceAndAddSymbol},// TODO
                {new StateTransition(SymbolType.Dot, SymbolType.Exclamation), MakePunctuationAndMakeSentenceAndAddSymbol}, //TODO
                {new StateTransition(SymbolType.Dot, SymbolType.Space), MakePunctuationAndMakeSentenceAndAddSymbol},

                {new StateTransition(SymbolType.Question, SymbolType.Letter), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Question, SymbolType.Digit), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Question, SymbolType.PunctuationMark), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Question, SymbolType.Dot), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Question, SymbolType.Question), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Question, SymbolType.Exclamation), MakePunctuationAndMakeSentenceAndAddSymbol}, // TODO
                {new StateTransition(SymbolType.Question, SymbolType.Space), MakePunctuationAndMakeSentenceAndAddSymbol},


                {new StateTransition(SymbolType.Space, SymbolType.Letter), MakeSpaceAndAddSymbol},
                {new StateTransition(SymbolType.Space, SymbolType.Digit), MakeSpaceAndAddSymbol},
                {new StateTransition(SymbolType.Space, SymbolType.PunctuationMark), MakeSpaceAndAddSymbol},
                {new StateTransition(SymbolType.Space, SymbolType.Dot), MakeSpaceAndAddSymbol},
                {new StateTransition(SymbolType.Space, SymbolType.Question), MakeSpaceAndAddSymbol},
                {new StateTransition(SymbolType.Space, SymbolType.Exclamation), MakeSpaceAndAddSymbol},
                {new StateTransition(SymbolType.Space, SymbolType.Space), SkipSymbol},


                {new StateTransition(SymbolType.Letter, SymbolType.End), EndWithLetterOrDigit},
                {new StateTransition(SymbolType.Digit, SymbolType.End), EndWithLetterOrDigit},
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
            SymbolChangeDelegate command;
            try
            {
                command = GetNext(nextSymbol);
                _currentState = nextSymbol;
            }
            catch (ArgumentException)
            {
                //command = null;
                throw;
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


        private void MakeWordAndPunctuationEndSentence(ISymbol nextSymbol,
            ref ICollection<ISymbol> symbols, ref ICollection<ISentenceElement> elements,
            ref ICollection<ISentence> sentences)
        {
            elements.Add(new Word(symbols));
            symbols.Clear();
            symbols.Add(nextSymbol);
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

            sentences.Add(new Sentence(elements));
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
