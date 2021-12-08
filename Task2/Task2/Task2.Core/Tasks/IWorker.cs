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
        void AllSentencesOrderedByWordsCount();

        void WordsFromQuestions(int wordLength);

        void DeleteWordsFromText(int wordLength);

        void ExchangeWordsInSentence(int sentenceNumber, int wordLength, string substring);
    }
}
