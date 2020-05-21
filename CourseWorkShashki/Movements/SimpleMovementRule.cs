namespace Movements
{
    using System;
    using Commands;
    using GameField;

    public class SimpleMovementRule : IMovementRule
    {
        public bool IsValid(Position from, Position to, out string reason)
        {
            if (from.Pawn == null)
            {
                reason = "No pawn is to be moved";
                return false;
            }
            if (Math.Abs(from.Y - to.Y) != 1 || from.X - to.X != from.Pawn.Color.ToDirection())
            {
                reason = "Invalid target position";;
                return false;
            }
            if (to.Pawn != null)
            {
                reason = "Target position is occupied";
                return false;
            }
            reason = "Valid move";
            return true;
        }
        
        public ICommand ToCommand(Position from, Position to)
        {
            return new MoveCommand(from, to);
        }
    }
}