namespace ConsoleApp
{
    using System;
    
    public class ConsoleProgram : IProgram
    {
        protected IConsoleMenu ConsoleMenu;
        private bool shouldProcessInput;
        
        public virtual void Init()
        {
            ConsoleMenu.PrintPrompt();
            shouldProcessInput = true;
        }

        public void ProcessInput()
        {
            while (shouldProcessInput)
            {
                var input = Console.ReadLine();
                if (ConsoleMenu.ValidateInput(input)) ConsoleMenu.RunCommand(input);
                else Console.WriteLine($"Invalid input: {input}");
                ConsoleMenu.PrintPrompt();
            }
        }
        
        public void ChangeMenu(IConsoleMenu consoleMenu)
        {
            ConsoleMenu = consoleMenu;
        }

        protected void StopProcessingInput()
        {
            shouldProcessInput = false;
        }
        
        public void Exit()
        {
            Console.WriteLine("Program is completed. Bye");
        }
    }
}