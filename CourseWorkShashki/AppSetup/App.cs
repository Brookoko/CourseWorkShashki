namespace AppSetup
{
    using Checkers.GameStatus;
    using ConsoleApp;
    using DependencyInjection;

    public class App : ConsoleProgram
    {
        [Inject]
        public IGameStatusProvider GameStatusProvider { get; set; }
        
        public App(IInjectionBinder binder)
        {
            var start = (StartMenu) binder.Inject(new StartMenu());
            var game = (GameMenu) binder.Inject(new GameMenu());
            start.AddOption("exit", _ => StopProcessingInput());
            start.AddOption("game", _ =>
            {
                GameStatusProvider.UpdateStatus(Status.WhiteMove);
                ChangeOptions(game);
            });
            ConsoleMenu = start;
        }
    }
}