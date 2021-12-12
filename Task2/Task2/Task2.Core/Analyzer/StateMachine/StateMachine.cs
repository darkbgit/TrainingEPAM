using System;
using System.Collections.Generic;
using System.Linq;
using Task2.Core.Model;
using Task2.Core.Model.Interfaces;
using Task2.Core.Model.Symbols;
using Task2.Core.Model.Symbols.ManySigns;

namespace Task2.Core.Analyzer.StateMachine
{
    internal class StateMachine : IStateMachine
    {
        private readonly Dictionary<StateTransition, Action<ISymbol>> _transitions;

        private readonly AnalyzerBuffer _buffer;

        internal StateMachine(AnalyzerBuffer buffer)
        {
            _buffer = buffer;
            _buffer.Symbols.Clear();
            _buffer.Symbols.Add(new BeginSymbol());

            _transitions = new Dictionary<StateTransition, Action<ISymbol>>
            {
                {new StateTransition(SymbolType.Begin, SymbolType.LetterOrDigit), AddSymbolAfterBegin},
                {new StateTransition(SymbolType.Begin, SymbolType.PunctuationMark), AddSymbolAfterBegin},
                {new StateTransition(SymbolType.Begin, SymbolType.Dot), AddSymbolAfterBegin}, //TODO
                {new StateTransition(SymbolType.Begin, SymbolType.Space), SkipSymbol},
                {new StateTransition(SymbolType.Begin, SymbolType.End), SkipSymbol},

                {new StateTransition(SymbolType.LetterOrDigit, SymbolType.LetterOrDigit), AddSymbol},
                {new StateTransition(SymbolType.LetterOrDigit, SymbolType.PunctuationMark), MakeWordAndAddSymbol},
                {new StateTransition(SymbolType.LetterOrDigit, SymbolType.Dot), MakeWordAndAddSymbol},
                {new StateTransition(SymbolType.LetterOrDigit, SymbolType.Question), MakeWordAndAddSymbol},
                {new StateTransition(SymbolType.LetterOrDigit, SymbolType.Exclamation), MakeWordAndAddSymbol},
                {new StateTransition(SymbolType.LetterOrDigit, SymbolType.Space), MakeWordAndAddSymbol},
                {new StateTransition(SymbolType.LetterOrDigit, SymbolType.End), EndWithLetterOrDigit},

                {new StateTransition(SymbolType.PunctuationMark, SymbolType.LetterOrDigit), MakeSentenceElementAndAddSymbol},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.PunctuationMark), MakeSentenceElementAndAddSymbol},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.Dot), MakeSentenceElementAndAddSymbol},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.Question), MakeSentenceElementAndAddSymbol},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.Exclamation), MakeSentenceElementAndAddSymbol},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.Space), MakeSentenceElementAndAddSymbol},
                {new StateTransition(SymbolType.PunctuationMark, SymbolType.End), EndMakeSentenceElementAndMakeSentence},

                {new StateTransition(SymbolType.Dot, SymbolType.LetterOrDigit), MakeSentenceElementAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Dot, SymbolType.PunctuationMark), MakeSentenceElementAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Dot, SymbolType.Dot), AddDoubleDot},
                {new StateTransition(SymbolType.Dot, SymbolType.Space), MakeSentenceElementAndMakeSentence},
                {new StateTransition(SymbolType.Dot, SymbolType.End), EndMakeSentenceElementAndMakeSentence},

                {new StateTransition(SymbolType.DoubleDot, SymbolType.Dot), AddEllipsis},

                {new StateTransition(SymbolType.Ellipsis, SymbolType.LetterOrDigit), MakeSentenceElementAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Ellipsis, SymbolType.PunctuationMark), MakeSentenceElementAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Ellipsis, SymbolType.Space), MakeSentenceElementAndMakeSentence},
                {new StateTransition(SymbolType.Ellipsis, SymbolType.End), EndMakeSentenceElementAndMakeSentence},

                {new StateTransition(SymbolType.Question, SymbolType.LetterOrDigit), MakeSentenceElementAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Question, SymbolType.PunctuationMark), MakeSentenceElementAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Question, SymbolType.Exclamation), AddQuestionWithExclamation},
                {new StateTransition(SymbolType.Question, SymbolType.Space), MakeSentenceElementAndMakeSentence},
                {new StateTransition(SymbolType.Question, SymbolType.End), EndMakeSentenceElementAndMakeSentence},

                {new StateTransition(SymbolType.QuestionWithExclamation, SymbolType.LetterOrDigit), MakeSentenceElementAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.QuestionWithExclamation, SymbolType.PunctuationMark), MakeSentenceElementAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.QuestionWithExclamation, SymbolType.Space), MakeSentenceElementAndMakeSentence},
                {new StateTransition(SymbolType.QuestionWithExclamation, SymbolType.End), EndMakeSentenceElementAndMakeSentence},

                {new StateTransition(SymbolType.Exclamation, SymbolType.LetterOrDigit), MakeSentenceElementAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Exclamation, SymbolType.PunctuationMark), MakeSentenceElementAndMakeSentenceAndAddSymbol},
                {new StateTransition(SymbolType.Exclamation, SymbolType.Space), MakeSentenceElementAndMakeSentence},
                {new StateTransition(SymbolType.Exclamation, SymbolType.End), EndMakeSentenceElementAndMakeSentence},


                {new StateTransition(SymbolType.Space, SymbolType.LetterOrDigit), MakeSentenceElementAndAddSymbol},
                {new StateTransition(SymbolType.Space, SymbolType.PunctuationMark), MakeSentenceElementAndAddSymbol},
                {new StateTransition(SymbolType.Space, SymbolType.Dot), MakeSentenceElementAndAddSymbol},
                {new StateTransition(SymbolType.Space, SymbolType.Space), SkipSymbol},
                {new StateTransition(SymbolType.Space, SymbolType.End), EndWithSpace}
            };
        }

        private Action<ISymbol> GetNext(SymbolType nextSymbol)
        {
            if (_buffer.Symbols.LastOrDefault() == null)
            {
                throw new NullReferenceException();
            }
            var transition = new StateTransition(_buffer.Symbols.Last().Type, nextSymbol);

            if (!_transitions.TryGetValue(transition, out var command))
                throw new StateMachineException(
                    $"Invalid transition: {_buffer.Symbols.Last().Type} -> {nextSymbol} in sentence {_buffer.Sentences.Count + 1}. Symbol skipped");
            return command;
        }

        public Action<ISymbol> MoveNext(ISymbol nextSymbol)
        {
            var command = GetNext(nextSymbol.Type);
            return command;
        }

        private void AddSymbol(ISymbol nextSymbol)
        {
            _buffer.Symbols.Add(nextSymbol);
        }

        private void AddSymbolAfterBegin(ISymbol nextSymbol)
        {
            _buffer.Symbols.Clear();
            _buffer.Symbols.Add(nextSymbol);
        }

        private void SkipSymbol(ISymbol nextSymbol)
        {

        }

        private void MakeSentenceElementAndAddSymbol(ISymbol nextSymbol)
        {
            try
            {
                _buffer.SentenceElements.Add((ISentenceElement)_buffer.Symbols.Last());
            }
            finally
            {
                _buffer.Symbols.Clear();
                _buffer.Symbols.Add(nextSymbol);
            }
        }

        private void MakeSentenceElementAndMakeSentenceAndAddSymbol(ISymbol nextSymbol)
        {
            try
            {
                _buffer.SentenceElements.Add((ISentenceElement)_buffer.Symbols.Last());
            }
            finally
            {
                _buffer.Symbols.Clear();
                _buffer.Symbols.Add(nextSymbol);
                _buffer.Sentences.Add(new Sentence(_buffer.SentenceElements));
                _buffer.SentenceElements.Clear();
            }
        }

        private void MakeSentenceElementAndMakeSentence(ISymbol nextSymbol)
        {
            try
            {
                _buffer.SentenceElements.Add((ISentenceElement)_buffer.Symbols.Last());
            }
            finally
            {
                _buffer.Symbols.Clear();
                _buffer.Symbols.Add(new BeginSymbol());
                _buffer.Sentences.Add(new Sentence(_buffer.SentenceElements));
                _buffer.SentenceElements.Clear();
            }
        }

        private void EndWithSpace(ISymbol nextSymbol)
        {
            if (_buffer.SentenceElements.Any())
            {
                _buffer.Sentences.Add(new Sentence(_buffer.SentenceElements));
            }
            _buffer.SentenceElements.Clear();
        }

        private void MakeWordAndAddSymbol(ISymbol nextSymbol)
        {
            try
            {
                _buffer.SentenceElements.Add(new Word(_buffer.Symbols));
            }
            finally
            {
                _buffer.Symbols.Clear();
                _buffer.Symbols.Add(nextSymbol);
            }
        }

        private void AddDoubleDot(ISymbol nextSymbol)
        {
            _buffer.Symbols.Clear();
            _buffer.Symbols.Add(new DoubleDot());
        }
        private void AddQuestionWithExclamation(ISymbol nextSymbol)
        {
            _buffer.Symbols.Clear();
            _buffer.Symbols.Add(new QuestionWithExclamation());
        }


        private void AddEllipsis(ISymbol nextSymbol)
        {
            _buffer.Symbols.Clear();
            _buffer.Symbols.Add(new Ellipsis());
        }

        private void EndMakeSentenceElementAndMakeSentence(ISymbol nextSymbol)
        {
            try
            {
                _buffer.SentenceElements.Add((ISentenceElement)_buffer.Symbols.Last());
            }
            finally
            {
                _buffer.Symbols.Clear();
                _buffer.Sentences.Add(new Sentence(_buffer.SentenceElements));
                _buffer.SentenceElements.Clear();
            }
        }

        private void EndWithLetterOrDigit(ISymbol nextSymbol)
        {
            _buffer.SentenceElements.Add(new Word(_buffer.Symbols));
            _buffer.Symbols.Clear();
            _buffer.Sentences.Add(new Sentence(_buffer.SentenceElements));
            _buffer.SentenceElements.Clear();
        }
    }
}
