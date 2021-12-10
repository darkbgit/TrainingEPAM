﻿namespace Task2.Core.IO.Consoles
{
    public interface ICommandLine
    {
        CommandLineCommand CommandLineArgumentParser(string[] args);

        string[] GetArguments();
    }
}
