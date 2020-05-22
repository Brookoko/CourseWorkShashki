namespace ConsoleApp
{
    public interface IProgram
    {
        void Init();
        
        void ProcessInput();

        void ChangeOptions(IConsoleMenu consoleMenu);

        void Exit();
    }
}