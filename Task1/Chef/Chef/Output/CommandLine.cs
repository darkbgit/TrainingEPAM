using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Output
{
    public class CommandLine : ICommandLine
    {
        private const string SORT_ON_CALORIC_CONTENT = "--s-k";
        private const string SORT_ON_INGREDIENT_NAME = "n";
        private const string SEARCH_ON_CALORIC_CONTENT_RANGE = "c";
        private const string EXIT = "--e";

        public CommandLineCommand CommandLineArgumentParser(string[] args)
        {
            switch (args.Length)
            {
                case 0:
                    return CommandLineCommand.Base;
                case 1:
                    return args[0] switch
                    {
                        CommandLineArguments.PRINT_INITIAL_DATA => CommandLineCommand.PrintData,
                        CommandLineArguments.EXIT => CommandLineCommand.Exit,
                        CommandLineArguments.SORT_ON_CALORIC_CONTENT => CommandLineCommand.SortOnCaloricContent,
                        CommandLineArguments.SORT_ON_INGREDIENT_NAME => CommandLineCommand.SortOnIngredientName,
                        _ => CommandLineCommand.UndefinedCommand
                    };
                case 3:
                    switch (args[0])
                    {
                        case CommandLineArguments.SEARCH_ON_CALORIC_CONTENT_RANGE:
                            if (int.TryParse(args[1], NumberStyles.Integer, CultureInfo.InvariantCulture,
                                    out int bottom) &&
                                int.TryParse(args[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out int top) &&
                                bottom >= 0 && bottom < top)
                            {
                                return CommandLineCommand.SearchOnCaloricContentRange;
                            }
                            return CommandLineCommand.UndefinedCommand;
                        default:
                            return CommandLineCommand.UndefinedCommand;
                    }
                default:
                    return CommandLineCommand.UndefinedCommand;
            }
        }
        
        public string[] GetArguments()
        {
            return Console.ReadLine()?.Split(' ').ToArray() 
                   ?? Array.Empty<string>();
        }

    }
}
