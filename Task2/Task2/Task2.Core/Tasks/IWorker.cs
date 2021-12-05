using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.Model.Interfaces;

namespace Task2.Core.Tasks
{
    public interface IWorker
    {
        /// <summary>
        /// Returns:
        /// IText where all sentences ordered by number of words in each sentence
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        IText AllSentencesOrderedByWordsCount(IText text);

        /// <summary>
        /// Returns:
        /// IEnumerable<string> from question sentences where 
        /// </summary>
        /// <param name="wordLength"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        IEnumerable<string> WordsFromQuestions(int wordLength, IText text);

        IText DeleteWordsFromText(int wordLength, IText text);

        IText ExchangeWordsInSentence(int sentenceNumber, int wordLength, string substring, IText text);

    }
}
