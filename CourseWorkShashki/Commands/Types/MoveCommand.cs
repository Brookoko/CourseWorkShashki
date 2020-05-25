namespace Checkers.Commands
{
    using GameStatus;
    using DependencyInjection;
    using GameField;

    public class MoveCommand : ICommand
    {
        [Inject]
        public IGameStatusProvider GameStatusProvider { get; set; }
        
        private readonly Position from;
        private readonly Position to;
        
        private bool turnedToDame;
        
        public MoveCommand(Position from, Position to)
        {
            this.from = from;
            this.to = to;
        }
        
        public void Execute()
        {
            to.Pawn = from.RemovePawn();
            turnedToDame = to.TryTurnToDame();
            GameStatusProvider.GoToNext();
        }

        public void Undo()
        {
            from.Pawn = to.RemovePawn();
            if (turnedToDame) from.Pawn.IsDame = false;
            GameStatusProvider.GoToNext();
        }
    }
}