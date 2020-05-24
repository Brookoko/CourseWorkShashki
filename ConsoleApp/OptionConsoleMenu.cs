namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class OptionConsoleMenu : IConsoleMenu
    {
        public readonly Options Options = new Options();
        
        public bool ValidateInput(string input)
        {
            return Options.ValidateCommand(input);
        }
        
        public void RunCommand(string input)
        {
            Options.Execute(input);
        }
        
        public void PrintPrompt()
        {
            foreach (var option in Options.GetOptions())
            {
                Console.WriteLine(option);
            }
            Console.Write("Command: ");
        }
    }
}