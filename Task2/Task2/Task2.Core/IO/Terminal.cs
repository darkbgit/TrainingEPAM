using System;
using System.IO;
using System.Text;
using Task2.Core.IO.Consoles;
using Task2.Core.Model.Interfaces;

namespace Task2.Core.IO
{
    public class Terminal : ITerminal //: IOutput, IInput

    {
    private readonly string _helpString;

    private readonly ICommandLine _commandLine;

    private readonly IOutput _output;

    public Terminal(ICommandLine commandLine, IOutput output)
    {
        _commandLine = commandLine;
        _output = output;
        _helpString = GenerateHelpString();
    }

    public void Print(string str)
    {
        _output.Print(str);
    }

    public void Print(IText text)
    {
        _output.Print(text);
    }

    public void Print(ISentence sentence)
    {
        _output.Print(sentence);
    }


    public void PrintHelp()
    {
        _output.Print(_helpString);
    }

    private static string GenerateHelpString()
    {
        var builder = new StringBuilder();
        builder.AppendLine("usage: " + Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName));
        var firstLength = builder.Length;
        builder.Append(' ', firstLength);
        builder.AppendLine($"[{CommandLineArguments.PrintData}] - вывод модели");
        builder.Append(' ', firstLength);
        builder.AppendLine($"[{CommandLineArguments.PrintAllSentencesOrderedByWordsCount}] - вывод всех предложений текста в порядке возрастания количества слов в каждом из них");
        builder.Append(' ', firstLength);
        builder.AppendLine($"[{CommandLineArguments.PrintDistinctWordsFromQuestionByWordLength} <wordLength>] - вывод всех не повторяющихся слов заданной длины из вопросительных предложений текста");
        builder.Append(' ', firstLength);
        builder.AppendLine(
            $"[{CommandLineArguments.DeleteWordsByWordLength} <wordLength>] - удалить все слова заданной длины, начинающихся на согласную букву");
        builder.Append(' ', firstLength);
        builder.AppendLine(
            $"[{CommandLineArguments.ExchangeWordsInSentenceBySubstring} <sentenceNumber> <wordLength> <substring>] - заменить все слова заданной длины из указанного предложения указанной подстрокой");
        builder.Append(' ', firstLength);
        builder.AppendLine(
            $"[{CommandLineArguments.SaveToFile} <filePath>] - сохранить модель в файл");
        builder.Append(' ', firstLength);
            builder.AppendLine($"[{CommandLineArguments.Exit}] - выход");
        return builder.ToString();
    }
    }
}
