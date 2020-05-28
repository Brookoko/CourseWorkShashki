namespace Checkers.AppSetup
{
    using GameStatus;
    using Commands;
    using ConsoleApp;
    using DependencyInjection;
    using GameField;

    public class App : ConsoleProgram
    {
        [Inject]
        public IGameStatusProvider GameStatusProvider { get; set; }
        
        [Inject]
        public IFieldProvider FieldProvider { get; set; }

        [Inject]
        public ICommandQueue CommandQueue { get; set; }
        
        public App(IInjectionBinder binder)
        {
            var start = (OptionConsoleMenu) binder.Inject(new OptionConsoleMenu());
            var game = (GameMenu) binder.Inject(new GameMenu());
            start.Options.AddOption("game", null, _ => CreateNewGame(game));
            start.Options.AddOption("exit", null, _ => StopProcessingInput());
            game.Options.AddOption("restart", null, _ => CreateNewGame(game));
            game.Options.AddOption("back", null, _ => ChangeMenu(start));
            ConsoleMenu = start;
        }
        
        private void CreateNewGame(GameMenu game)
        {
            CommandQueue.Reset();
            FieldProvider.CreateNew();
            GameStatusProvider.UpdateStatus(Status.WhiteMove);
            ChangeMenu(game);
        }
    }
}