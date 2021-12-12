using System.IO;
using System.Linq;
using Task2.Core.IO;
using Task2.Core.IO.Files;
using Task2.Core.Model;
using Task2.Core.Model.Interfaces;
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
            var query = new GetAllSentencesOrderedByWordCountQuery(_text);
            var result = query.Execute();

            _output.Print("");
            _output.Print("Все предложения текста в порядке возрастания количества слов в каждом из них");
            _output.Print("");

            foreach (var sentence in result)
            {
                _output.Print($"{sentence} {sentence.WordsCount} слов");
            }
        }

        public void WordsFromQuestions(int wordLength)
        {
            var query = new GetAllWordsFromQuestionsQuery(_text);
            var result = query.Execute(wordLength);

            _output.Print("");
            _output.Print("Все слова заданной длины без повторений из вопросительных предложений текста");
            _output.Print("");

            var words = result as Word[] ?? result.ToArray();

            if (words.Any())
            {
                foreach (var word in words)
                {
                    _output.Print(word.ToString());
                }
            }
            else
            {
                _output.Print("Слова не найдены");
            }
        }

        public void DeleteWordsFromText(int wordLength)
        {
            var command = new DeleteWordsByLengthCommand(_text);
            command.Execute(wordLength);

            _output.Print("");
            _output.Print("Удалить все слова заданной длины, начинающихся на согласную букву");
            _output.Print("");

            _output.Print(_text);
        }

        public void ExchangeWordsInSentence(int sentenceNumber, int wordLength, string substring)
        {
            var command = new ExchangeWordsBySubstring(_text);
            command.Execute(sentenceNumber, wordLength, substring);

            _output.Print("");
            _output.Print("Все слова заданной длины заменить указанной подстрокой");
            _output.Print("");
            _output.Print(_text);
        }

        public void SaveToFile(string filePath)
        {
            IOutput outputFile = new OutputToFile(filePath);

            outputFile.Print(_text);

            var fullFilePath = Path.GetFullPath(filePath);
            _output.Print($"Файл {fullFilePath} сохранен");

        }
    }
}
