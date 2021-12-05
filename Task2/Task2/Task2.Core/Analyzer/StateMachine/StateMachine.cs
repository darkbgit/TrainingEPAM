using System;
using System.Collections.Generic;
using System.Linq;
using Task2.Core.Model;
using Task2.Core.Model.Interfaces;
using Task2.Core.Model.Symbols;
using Task2.Core.Model.Symbols.ManySigns;
using Task2.Core.Model.Symbols.OneSign;

namespace Task2.Core.Analyzer.StateMachine
{

    //internal delegate void SymbolChangeDelegate(ISymbol nextSymbol, ref AnalyzerBuffer buffer);

    internal class StateMachine : IStateMachine
    {
        private readonly Dictionary<StateTransition, Action<ISymbol>> _transitions;

        private readonly AnalyzerBuffer _buffer;

        private ISymbol _currentSymbol;

        internal StateMachine()
        {
            _currentSymbol = new NoSymbol();
            _buffer = new AnalyzerBuffer();
            _transitions = new Dictionary<StateTransition, Action<ISymbol>>
            {
                {new StateTransition(SymbolType.NoSymbol, SymbolType.LetterOrDigit), AddSymbol},
                {new StateTransition(SymbolType.NoSymbol, SymbolType.PunctuationMark), AddSymbol},
                {new StateTransition(SymbolType.NoSymbol, SymbolType.Dot), AddSymbol}, //TODO
                {new StateTransition(SymbolType.NoSymbol, SymbolType.Space), SkipSymbol},
                {new StateTransition(SymbolType.NoSymbol, SymbolType.NoSymbol), SkipSymbol},

                {new StateTransition(SymbolType.LetterOrDigit, SymbolType.LetterOrDigit), AddSymbol},
                {new StateTransition(SymbolType.LetterOrDigit, SymbolType.PunctuationMark), MakeWordAndAddSymbol},
                {new StateTransition(SymbolType.LetterOrDigit, SymbolType.Dot), MakeWordAndAddSymbol},
                {new StateTransition(SymbolType.LetterOrDigit, SymbolType.Question), MakeWordAndAddSymbol},
                {new StateTransition(SymbolType.LetterOrDigit, SymbolType.Exclamation), MakeWordAndAddSymbol},
                {new StateTransition(SymbolType.LetterOrDigit, SymbolType.Space), MakeWordAndAddSymbol},
                {new StateTransition(SymbolType.LetterOrDigit, SymbolType.NoSymbol), EndWithLetterOrDigit},

                {new StateTransition(SymbolType.PunctuationMark, SymbolType.LetterOrDigit), MakePunctuationAndAddSymbol},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.PunctuationMark), MakePunctuationAndAddSymbol},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.Dot), MakePunctuationAndAddSymbol},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.Question), MakePunctuationAndAddSymbol},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.Exclamation), MakePunctuationAndAddSymbol},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.Space), MakePunctuationAndAddSymbol},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.NoSymbol), EndWithPunctuation},

                {new StateTransition(SymbolType.Dot, SymbolType.LetterOrDigit), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Dot, SymbolType.PunctuationMark), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Dot, SymbolType.Dot), MakeDoubleDot},
                {new StateTransition(SymbolType.Dot, SymbolType.Space), MakePunctuationAndMakeSentence},
                {new StateTransition(SymbolType.Dot, SymbolType.NoSymbol), EndWithPunctuationEndSentence},

                {new StateTransition(SymbolType.DoubleDot, SymbolType.Dot), MakeEllipsis},

                {new StateTransition(SymbolType.Ellipsis, SymbolType.LetterOrDigit), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Ellipsis, SymbolType.PunctuationMark), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Ellipsis, SymbolType.Space), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Ellipsis, SymbolType.NoSymbol), EndWithPunctuationEndSentence},

                {new StateTransition(SymbolType.Question, SymbolType.LetterOrDigit), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Question, SymbolType.PunctuationMark), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Question, SymbolType.Dot), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Question, SymbolType.Exclamation), MakeQuestionWithExclamation},
                {new StateTransition(SymbolType.Question, SymbolType.Space), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Question, SymbolType.NoSymbol), EndWithPunctuationEndSentence},

                {new StateTransition(SymbolType.QuestionWithExclamation, SymbolType.LetterOrDigit), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.QuestionWithExclamation, SymbolType.PunctuationMark), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.QuestionWithExclamation, SymbolType.Space), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.QuestionWithExclamation, SymbolType.NoSymbol), EndWithPunctuationEndSentence},

                {new StateTransition(SymbolType.Exclamation, SymbolType.LetterOrDigit), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Exclamation, SymbolType.PunctuationMark), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Exclamation, SymbolType.Space), MakePunctuationAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Exclamation, SymbolType.NoSymbol), EndWithPunctuationEndSentence},


                {new StateTransition(SymbolType.Space, SymbolType.LetterOrDigit), MakeSpaceAndAddSymbol},
                {new StateTransition(SymbolType.Space, SymbolType.PunctuationMark), MakeSpaceAndAddSymbol},
                {new StateTransition(SymbolType.Space, SymbolType.Dot), MakeSpaceAndAddSymbol},
                {new StateTransition(SymbolType.Space, SymbolType.Space), SkipSymbol},
                {new StateTransition(SymbolType.Space, SymbolType.NoSymbol), EndWithSpace}
            };
        }

        private Action<ISymbol> GetNext(SymbolType nextSymbol)
        {
            var transition = new StateTransition(_currentSymbol.Type, nextSymbol);

            if (!_transitions.TryGetValue(transition, out var command))
                throw new ArgumentException("Invalid transition: " + _currentSymbol.Type + " -> " + nextSymbol);

            return command;
        }

        public Action<ISymbol> MoveNext(ISymbol nextSymbol)
        {
            var command = GetNext(nextSymbol.Type);
            _currentSymbol = nextSymbol;

            return command;
        }

        private void AddSymbol(ISymbol nextSymbol)
        {
            _buffer.Symbols.Add(nextSymbol);
        }
        
        private void SkipSymbol(ISymbol nextSymbol)
        {

        }

        private void MakeSpaceAndAddSymbol(ISymbol nextSymbol)
        {
            _buffer.SentenceElements.Add(new Space());
            _buffer.Symbols.Clear();
            _buffer.Symbols.Add(nextSymbol);
        }

        private void MakeWordAndAddSymbol(ISymbol nextSymbol)
        {
            _buffer.SentenceElements.Add(new Word(_buffer.Symbols));
            _buffer.Symbols.Clear();
            _buffer.Symbols.Add(nextSymbol);
        }


        private void MakeDoubleDot(ISymbol nextSymbol)
        {
            _buffer.Symbols.Clear();
            _buffer.Symbols.Add(new DoubleDot());
        }


        private void MakeEllipsis(ISymbol nextSymbol)
        {
            _buffer.Symbols.Clear();
            _buffer.Symbols.Add(new Ellipsis());
        }

        private void MakeQuestionWithExclamation(ISymbol nextSymbol)
        {
            _buffer.Symbols.Clear();
            _buffer.Symbols.Add(new QuestionWithExclamation());
        }

        private void MakePunctuationAndAddSymbol(ISymbol nextSymbol)
        {
            _buffer.SentenceElements.Add(new PunctuationMark(_buffer.Symbols.Last()));
            _buffer.Symbols.Clear();
            _buffer.Symbols.Add(nextSymbol);
        }


        private void MakePunctuationAndMakeSentenceAndAddSymbol(ISymbol nextSymbol)
        {
            _buffer.SentenceElements.Add(new PunctuationMark(_buffer.Symbols));
            _buffer.Symbols.Clear();
            _buffer.Sentences.Add(new Sentence(_buffer.SentenceElements));
            _buffer.SentenceElements.Clear();
            _buffer.Symbols.Add(nextSymbol);
        }

        private void MakePunctuationAndMakeSentence(ISymbol nextSymbol)
        {
            _buffer.SentenceElements.Add(new PunctuationMark(_buffer.Symbols));
            _buffer.Symbols.Clear();
            _buffer.Sentences.Add(new Sentence(_buffer.SentenceElements));
            _buffer.SentenceElements.Clear();
            _currentSymbol = new NoSymbol();
        }


        private void EndWithLetterOrDigit(ISymbol nextSymbol)
        {
            _buffer.SentenceElements.Add(new Word(_buffer.Symbols));
            _buffer.Symbols.Clear();
            _buffer.Sentences.Add(new Sentence(_buffer.SentenceElements));
            _buffer.SentenceElements.Clear();
        }

        private void EndWithPunctuation(ISymbol nextSymbol)
        {
            _buffer.SentenceElements.Add(new Word(_buffer.Symbols));
            _buffer.Symbols.Clear();
            _buffer.SentenceElements.Add(new PunctuationMark(nextSymbol));
            _buffer.Sentences.Add(new Sentence(_buffer.SentenceElements));
            _buffer.SentenceElements.Clear();
        }

        private void EndWithSpace(ISymbol nextSymbol)
        {
            if (_buffer.SentenceElements.Any())
            {
                _buffer.Sentences.Add(new Sentence(_buffer.SentenceElements));
            }
            _buffer.SentenceElements.Clear();
        }

        private void EndWithPunctuationEndSentence(ISymbol nextSymbol)
        {
            _buffer.SentenceElements.Add(new PunctuationMark(_buffer.Symbols));
            _buffer.Symbols.Clear();
            _buffer.Sentences.Add(new Sentence(_buffer.SentenceElements));
            _buffer.SentenceElements.Clear();
        }

    }


}
