using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Core.TextParts.Interfaces
{
    public interface ISentence : IEnumerable<ISentenceElement>
    {
        int WordsCount { get; }
    }
}
