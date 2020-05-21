namespace Movements
{
    using System;
    using Commands;
    using GameField;

    public class SimpleMovementRule : IMovementRule
    {
        public string Name => "Move";
        
        public bool IsValid(Position from, Position to, Field field, out string reason)
        {
            if (from.Pawn == null)
            {
                reason = "No pawn is to be moved";
                return false;
            }
            if (Math.Abs(to.Y - from.Y) != 1 || to.X - from.X != from.Pawn.Color.ToDirection())
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
        
        public ICommand ToCommand(Position from, Position to, Field field)
        {
            return new MoveCommand(from, to);
        }
    }
}