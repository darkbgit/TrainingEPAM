using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.Model.Interfaces;


namespace Task2.Core.Analyzer
{
    public interface IAnalyzer
    {
        IText Analyze();
    }
}
