using System;
using System.Collections.Generic;
using System.Linq;
using Task2.Core.Analyzer;
using Task2.Core.Model;
using Task2.Core.Model.Interfaces;

namespace Task2.Core.Tasks
{
    public class TasksWorker : IWorker
    {
        private readonly IAnalyzer _analyzer;


        public TasksWorker(IAnalyzer analyzer)
        {
            _analyzer = new TextAnalyzer();
        }


        public void AllSentencesOrderedByWordsCount(IText text)
        {
            //_output.Print("");
            //_output.Print("Все предложения текста в порядке возрастания количества слов в каждом из них");
            //_output.Print("");
            var result = string.Join(Environment.NewLine, text.OrderBy(s => s.WordsCount)
                .Select(i => i + " Количество слов - " + i.WordsCount));
            //_output.Print(result);
        }

        IEnumerable<string> IWorker.WordsFromQuestions(int wordLength, IText text)
        {
            throw new NotImplementedException();
        }

        IText IWorker.DeleteWordsFromText(int wordLength, IText text)
        {
            throw new NotImplementedException();
        }

        public IText ExchangeWordsInSentence(int sentenceNumber, int wordLength, string substring, IText text)
        {
            throw new NotImplementedException();
        }


        IText IWorker.AllSentencesOrderedByWordsCount(IText text)
        {
            throw new NotImplementedException();
        }

        public void WordsFromQuestions(int wordLength, IText text)
        {
            //var result = text.Select(s => s.Last() is Question)
        }


        public void DeleteWordsFromText(int wordLength, IText text)
        {
            //_output.Print("");
            //_output.Print($"Текст без слов длинной {wordLength}, начинающихся на согласную букву");
            //_output.Print("");
            var result = text.Select(s => new Sentence(s
                .Where(e => e is not Word word ||
                            word.Length() != wordLength &&
                            Consonant.IsConsonantChar(word.FirstOrDefault()?.ToString()?[0]))));
            //_output.Output(new Text(result));
        }


        public void ExchangeWordsInSentence(int sentenceNumber, int wordLength)
        {

        }
    }
}
