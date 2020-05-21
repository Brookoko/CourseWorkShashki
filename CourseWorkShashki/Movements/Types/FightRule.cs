namespace Movements
{
    using System;
    using Checkers;
    using Commands;
    using GameField;
    
    public class FightRule : IMovementRule
    {
        public string Name => "Fight";
        
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
            if (!Utils.IsDiagonal(from, to, 2))
            {
                reason = "Invalid target position";
                return false;
            }
            if (field.OpponentPawnOnLine(from, to) == null)
            {
                reason = "No opponent pawn in the way";
                return false;
            }
            reason = "Valid move";
            return true;
        }
        
        public ICommand ToCommand(Position from, Position to, Field field)
        {
            return new FightCommand(from, to, field.OpponentPawnOnLine(from, to));
        }
    }
}