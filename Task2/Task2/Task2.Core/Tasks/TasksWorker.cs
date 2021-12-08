using System;
using System.Collections.Generic;
using System.Linq;
using Task2.Core.Analyzer;
using Task2.Core.Model;
using Task2.Core.Model.Interfaces;
using Task2.Core.Output;
using Task2.Core.Tasks.Commands;
using Task2.Core.Tasks.Queries;

namespace Task2.Core.Tasks
{
    public class TasksWorker : IWorker
    {
        private readonly IText _text;
        private readonly IOutput _output;

        public TasksWorker(IText text, IOutput output)
        {
            _text = text;
            _output = output;
        }

        public void AllSentencesOrderedByWordsCount()
        {
            _output.Print("");
            _output.Print("Все предложения текста в порядке возрастания количества слов в каждом из них");
            _output.Print("");
            var query = new GetAllSentencesOrderedByWordCountQuery(_text);
            var result = query.Execute();
            foreach (var sentence in result)
            {
                _output.Print($"{sentence} {sentence.WordsCount} слов");
            }
        }

        public void WordsFromQuestions(int wordLength)
        {
            _output.Print("");
            _output.Print("Все слова заданной длины без повторений из вопросительных предложений текста");
            _output.Print("");
            var query = new GetAllWordsFromQuestionsQuery(_text);
            var result = query.Execute(wordLength);
            foreach (var word in result)
            {
                _output.Print(word.ToString());
            }
        }

        public void DeleteWordsFromText(int wordLength)
        {
            _output.Print("");
            _output.Print("Удалить все слова заданной длины, начинающихся на согласную букву");
            _output.Print("");

            var command = new DeleteWordsByLengthCommand(_text);
            command.Execute(wordLength);

            _output.Print(_text);

        }

        public void ExchangeWordsInSentence(int sentenceNumber, int wordLength, string substring)
        {
            _output.Print("");
            _output.Print("Все слова заданной длины заменить указанной подстрокой");
            _output.Print("");

            var command = new ExchangeWordsBySubstring(_text);
            command.Execute(sentenceNumber, wordLength, substring);

            _output.Print(_text);
        }
    }
}
