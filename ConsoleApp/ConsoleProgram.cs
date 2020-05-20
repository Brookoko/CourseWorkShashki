namespace ConsoleApp
{
    using System;
    
    public class ConsoleProgram : IProgram
    {
        protected IOptions Options;
        private bool shouldProcessInput;
        
        public virtual void Init()
        {
            PrintOptions(Options);
            shouldProcessInput = true;
        }

        public void ProcessInput()
        {
            while (shouldProcessInput)
            {
                var option = Console.ReadLine();
                if (Options.ValidateInput(option))
                {
                    Options.RunOption(option);
                    continue;
                }
                Console.WriteLine($"No option for input: {option}. Try one of these:");
                PrintOptions(Options);
            }
        }
        
        public void ChangeOptions(IOptions options)
        {
            Options = options;
            PrintOptions(Options);
        }

        protected void StopProcessingInput()
        {
            shouldProcessInput = false;
        }
        
        private void PrintOptions(IOptions options)
        {
            foreach (var option in options.OptionsList())
            {
                Console.WriteLine($"{option}");
            }
            Console.WriteLine();
        }
        
        public void Exit()
        {
            Console.WriteLine("Program is completed. Bye");
        }
    }
}