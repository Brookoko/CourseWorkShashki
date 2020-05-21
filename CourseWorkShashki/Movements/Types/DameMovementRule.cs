namespace Movements
{
    using Checkers;
    using Commands;
    using GameField;

    public class DameMovementRule : IMovementRule
    {
        public string Name => "DameMove";
        
        public bool IsValid(Position from, Position to, Field field, out string reason)
        {
            if (!Utils.IsStraightLine(from, to) && !Utils.IsDiagonal(from, to))
            {
                reason = "Invalid movement direction";
                return false;
            }
            var pawns = field.PawnsOnLine(from, to);
            if (pawns.Count > 0 || to.Pawn != null)
            {
                reason = "Pawns are in the way";
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