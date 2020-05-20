namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;

    public interface IOptions
    {
        string Id { get; }
        
        void AddOption(string option, Action<Parameters> action);

        bool ValidateInput(string option);
        
        void RunOption(string option);
        
        IEnumerable<string> OptionsList();
    }
}