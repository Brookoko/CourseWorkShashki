namespace Checkers.AppSetup
{
    using System;
    using Checkers;
    using GameStatus;
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
            if (GameStatusProvider.Status.IsWinStatus()) return false;
            var (from, to) = converter.ToPositions(input, FieldProvider.Field);
            return ValidPositions(from, to) && IsValidMove(from, to);
        }
        
        private bool ValidPositions(Position from, Position to)
        {
            if (from == null) Console.WriteLine("Invalid start position");
            else if (to == null) Console.WriteLine("Invalid target position");
            return from != null && to != null;
        }
        
        private bool IsValidMove(Position from, Position to)
        {
            var move = CreateMove(from, to);
            if (MovementProvider.IsValid(move)) return true;
            Console.WriteLine(move.RejectionReason);
            return false;
        }

        private Move CreateMove(Position from, Position to)
        {
            return new Move
            {
                From = from,
                To = to,
                Field = FieldProvider.Field,
                Status = GameStatusProvider.Status
            };
        }
        
        public void RunCommand(string input)
        {
            if (Options.ValidateCommand(input))
            {
                Options.Execute(input);
                return;
            }
            var (from, to) = converter.ToPositions(input, FieldProvider.Field);
            var move = CreateMove(from, to);
            CommandQueue.Execute(MovementProvider.ToCommand(move));
        }
        
        public void PrintPrompt()
        {
            drawer.Draw(FieldProvider.Field);
            foreach (var option in Options.GetOptions())
            {
                Console.WriteLine(option);
            }
            Console.Write(ToPrompt(GameStatusProvider.Status));
        }
        
        private string ToPrompt(Status status)
        {
            switch (status)
            {
                case Status.Menu: return "Menu: ";
                case Status.WhiteMove: return "Move (white): ";
                case Status.BlackMove: return "Move (black): ";
                case Status.WhiteAttack: return "Attack (white): ";
                case Status.BlackAttack: return "Attack (black): ";
                case Status.WhiteWin: return "Congratulation White win\nType restart to try one more time or back to return to start menu\n";
                case Status.BlackWin: return "Congratulation Black win\nType restart to try one more time or back to return to start menu\n";
                default: return "";
            }
        }
    }
}