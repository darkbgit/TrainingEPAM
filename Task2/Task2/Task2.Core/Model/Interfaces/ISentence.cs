using System.Collections.Generic;

namespace Task2.Core.Model.Interfaces
{
    public interface ISentence : IEnumerable<ISentenceElement>
    {
        int WordsCount { get; }

        void RemoveAt(int index);

        void AddRangeAt(int index, IEnumerable<ISentenceElement> elements);
    }
}
