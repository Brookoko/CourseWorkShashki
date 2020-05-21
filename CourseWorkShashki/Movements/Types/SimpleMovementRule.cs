namespace Movements
{
    using System;
    using Checkers;
    using Commands;
    using GameField;

    public class SimpleMovementRule : IMovementRule
    {
        public string Name => "SimpleMove";
        
        public bool IsValid(Position from, Position to, Field field, out string reason)
        {
            if (from.Pawn == null)
            {
                reason = "No pawn is to be moved";
                return false;
            }
            if (from.Pawn.IsDame)
            {
                reason = "Pawn is a dame";
                return false;
            }
            if (!Utils.IsDiagonalInDirection(from, to, from.Pawn.Color.ToDirection()))
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