namespace ConsoleApp
{
    public interface IProgram
    {
        void Init();
        
        void ProcessInput();

        void ChangeMenu(IConsoleMenu consoleMenu);

        void Exit();
    }
}