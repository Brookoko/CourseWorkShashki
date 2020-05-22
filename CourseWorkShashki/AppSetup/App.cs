namespace AppSetup
{
    using ConsoleApp;
    using DependencyInjection;

    public class App : ConsoleProgram
    {
        public App(IInjectionBinder binder)
        {
            var start = (StartMenu) binder.Inject(new StartMenu());
            var game = (GameMenu) binder.Inject(new GameMenu());
            start.AddOption("exit", _ => StopProcessingInput());
            start.AddOption("game", _ => ChangeOptions(game));
            ConsoleMenu = start;
        }
    }
}