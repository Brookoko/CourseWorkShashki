namespace AppSetup
{
    using Checkers.GameStatus;
    using ConsoleApp;
    using DependencyInjection;
    using GameField;

    public class App : ConsoleProgram
    {
        [Inject]
        public IGameStatusProvider GameStatusProvider { get; set; }
        
        [Inject]
        public IFieldProvider FieldProvider { get; set; }
        
        public App(IInjectionBinder binder)
        {
            var start = (StartMenu) binder.Inject(new StartMenu());
            var game = (GameMenu) binder.Inject(new GameMenu());
            start.AddOption("exit", _ => StopProcessingInput());
            start.AddOption("game", _ =>
            {
                FieldProvider.CreateNew();
                GameStatusProvider.UpdateStatus(Status.WhiteMove);
                ChangeOptions(game);
            });
            ConsoleMenu = start;
        }
    }
}