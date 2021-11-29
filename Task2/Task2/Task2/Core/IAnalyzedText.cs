using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.TextParts.Interfaces;

namespace Task2.Core
{
    public interface IAnalyzedText : IEnumerable<ISentence>
    {
    }
}
