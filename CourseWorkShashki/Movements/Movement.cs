namespace Movements
{
    using System;
    using Commands;
    using GameField;

    public class Movement
    {
        private readonly Position from;
        private readonly Position to;
        
        public Movement(Position from, Position to)
        {
            this.from = from;
            this.to = to;
        }
        
        public bool IsValid()
        {
            if (from.Pawn == null) return false;
            if (Math.Abs(from.Y - to.Y) != 1) return false;
            var direction = from.Pawn.Color == Color.Black ? -1 : 1;
            if (from.X - to.X != direction) return false;
            return to.Pawn == null;
        }
        
        public ICommand ToCommand()
        {
            return new MoveCommand(from, to);
        }
    }
}