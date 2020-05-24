namespace AppSetup
{
    using System;
    using Checkers;
    using Checkers.GameStatus;
    using Commands;
    using ConsoleApp;
    using DependencyInjection;
    using GameField;
    using Movements;

    public class GameMenu : IConsoleMenu
    {
        [Inject]
        public IMovementProvider MovementProvider { get; set; }
        
        [Inject]
        public ICommandQueue CommandQueue { get; set; }
        
        [Inject]
        public IGameStatusProvider GameStatusProvider { get; set; }
        
        [Inject]
        public IFieldProvider FieldProvider { get; set; }
        
        public readonly Options Options = new Options();
        private readonly Drawer drawer = new Drawer();
        private readonly StringPositionConverter converter = new StringPositionConverter();
        
        public GameMenu()
        {
            Options.AddOption("undo", null, _ => CommandQueue.Undo());
        }
        
        public bool ValidateInput(string input)
        {
            if (Options.ValidateCommand(input)) return true;
            var (from, to) = converter.ToPositions(input, FieldProvider.Field);
            return ValidPositions(from, to) && TryGetMovement(from, to, out _);
        }
        
        private bool ValidPositions(Position from, Position to)
        {
            if (from == null) Console.WriteLine("Invalid start position");
            else if (to == null) Console.WriteLine("Invalid target position");
            return from != null && to != null;
        }
        
        private bool TryGetMovement(Position from, Position to, out IMovementRule rule)
        {
            rule = MovementProvider.RuleFor(from, to, FieldProvider.Field);
            if (rule.IsValid()) return true;
            Console.WriteLine(rule.Reason);
            return false;
        }

        public void RunCommand(string input)
        {
            if (Options.ValidateCommand(input))
            {
                Options.Execute(input);
                return;
            }
            var (from, to) = converter.ToPositions(input, FieldProvider.Field);
            var rule = MovementProvider.RuleFor(from, to, FieldProvider.Field);
            CommandQueue.Execute(rule.ToCommand());
        }
        
        public void PrintPrompt()
        {
            drawer.Draw(FieldProvider.Field);
            foreach (var option in Options.GetOptions())
            {
                Console.WriteLine(option);
            }
            Console.Write(GameStatusProvider.Status.ToPrompt());
        }
    }
}