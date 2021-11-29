using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core;
using Task2.Core.TextObjectModel.Interfaces;
using Task2.Output;

namespace Task2
{
    public class TasksWorker
    {
        private readonly ILogger _logger;

        public TasksWorker(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Output all sentences ordered by the number of words in each sentence 
        /// </summary>
        public void AllSentencesOrderedByWordsCount(IText text)
        {
            //var sortedText = text.OrderBy(s => s.WordsCount).ToList();
            var result = string.Join(Environment.NewLine, text.OrderBy(s => s.WordsCount).ToList());
            _logger.Print(result);
        }

        
        public void WordsFromQuestions(int wordLength, IText text)
        {
            //var result = string.Join(Environment.NewLine, text.SelectMany(s => s.))
        }
        

        public void DeleteWordsFromText(int wordLength)
        {
            //var result = 
        }

        public void ExchangeWordsInSentence(int sentenceNumber, int wordLength)
        {

        }
    }
}
