namespace ConsoleApp
{
    public interface IConsoleMenu
    {
        bool ValidateInput(string input);
        
        void RunCommand(string input);
        
        void PrintPrompt();
    }
}