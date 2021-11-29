using System.Collections.Generic;

namespace Task2.Core.TextObjectModel.Interfaces
{
    public interface ISentence : IEnumerable<ISentenceElement>
    {
        int WordsCount { get; }
    }
}
