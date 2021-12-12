namespace Task2.Core.Tasks
{
    public interface IWorker
    {
        void AllSentencesOrderedByWordsCount();

        void WordsFromQuestions(int wordLength);

        void DeleteWordsFromText(int wordLength);

        void ExchangeWordsInSentence(int sentenceNumber, int wordLength, string substring);

        void SaveToFile(string filePath);
    }
}
