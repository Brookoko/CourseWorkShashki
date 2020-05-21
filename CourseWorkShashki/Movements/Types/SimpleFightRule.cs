namespace Movements
{
    using System;
    using Checkers;
    using Commands;
    using GameField;
    
    public class SimpleFightRule : IMovementRule
    {
        public string Name => "Fight";
        
        public bool IsValid(Position from, Position to, Field field, out string reason)
        {
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