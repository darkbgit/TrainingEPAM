using System;
using System.Collections.Generic;
using System.Linq;
using Task2.Core.Analyzer;
using Task2.Core.CQRS.Commands;
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
            foreach (var sentence in result)
            {
                _output.Print($"{sentence} {sentence.WordsCount} слов");
            }
        }

        public void WordsFromQuestions(int wordLength, IText text)
        {
            _output.Print("");
            _output.Print("Все слова заданной длины без повторений из вопросительных предложений текста");
            _output.Print("");
            var query = new GetAllWordsFromQuestionsQuery(text);
            var result = query.Execute(wordLength);
            foreach (var word in result)
            {
                _output.Print(word.ToString());
            }
        }


        public void DeleteWordsFromText(int wordLength, IText text)
        {
            _output.Print("");
            _output.Print("Удалить все слова заданной длины, начинающихся на согласную букву");
            _output.Print("");

            var command = new DeleteWordsByLengthCommand(text);
            command.Execute(wordLength);

        }

        public void ExchangeWordsInSentence(int sentenceNumber, int wordLength, string substring, IText text)
        {
            throw new NotImplementedException();
        }
    }
}
