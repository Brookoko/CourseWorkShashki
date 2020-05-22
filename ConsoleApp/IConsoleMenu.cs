namespace ConsoleApp
{
    public interface IConsoleMenu
    {
        string Id { get; }
        
        bool ValidateInput(string command);
        
        void RunCommand(string command);
        
        void PrintPrompt();
    }
}