namespace Commands
{
    using GameField;

    public class MoveCommand : ICommand
    {
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
        }

        public void Undo()
        {
            from.Pawn = to.RemovePawn();
            if (turnedToDame) from.Pawn.IsDame = false;
        }
    }
}