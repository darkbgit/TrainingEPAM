using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Core.Analyzer
{
    public interface IAnalyzer
    {
        IAnalyzedText Analyze(string text);
    }
}
