using System;
using System.Collections.Generic;
using System.Linq;
using Task2.Core.Analyzer;
using Task2.Core.CQRS.Queries;
using Task2.Core.Model;
using Task2.Core.Model.Interfaces;
using Task2.Core.Output;

namespace Task2.Core.Tasks
{
    public class TasksWorker : IWorker
    {
        //private readonly IAnalyzer _analyzer;
        private readonly IOutput _output;


        public TasksWorker(IOutput output)
        {
            _output = output;
            //_analyzer = new TextAnalyzer();
        }


        public void AllSentencesOrderedByWordsCount(IText text)
        {
            _output.Print("");
            _output.Print("Все предложения текста в порядке возрастания количества слов в каждом из них");
            _output.Print("");
            var query = new GetAllSentencesOrderedByWordCountQuery(text);
            var result = query.Execute();
            _output.Print(result);
        }

        public void WordsFromQuestions(int wordLength, IText text)
        {
            //var result = text.Select(s => s.Last() is Question)
            _output.Print("");
            _output.Print("Все слова предложения текста в порядке возрастания количества слов в каждом из них");
            _output.Print("");
            var query = new GetAllSentencesOrderedByWordCountQuery(text);
            var result = query.Execute();
            _output.Print(result);
        }

        IText IWorker.DeleteWordsFromText(int wordLength, IText text)
        {
            throw new NotImplementedException();
        }

        public IText ExchangeWordsInSentence(int sentenceNumber, int wordLength, string substring, IText text)
        {
            throw new NotImplementedException();
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
