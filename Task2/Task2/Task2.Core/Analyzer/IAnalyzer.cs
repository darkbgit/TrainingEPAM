using System;
using Task2.Core.Model.Interfaces;


namespace Task2.Core.Analyzer
{
    public interface IAnalyzer : IDisposable
    {
        IText Analyze();
    }
}
