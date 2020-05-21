namespace Commands
{
    using GameField;

    public class MoveCommand : ICommand
    {
        private readonly Position from;
        private readonly Position to;
        
        public MoveCommand(Position from, Position to)
        {
            this.from = from;
            this.to = to;
        }
        
        public void Execute()
        {
            to.Pawn = from.RemovePawn();
        }

        public void Undo()
        {
            from.Pawn = to.RemovePawn();
        }
    }
}