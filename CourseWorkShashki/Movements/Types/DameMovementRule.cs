namespace Movements
{
    using Checkers;
    using Commands;
    using GameField;

    public class DameMovementRule : IMovementRule
    {
        public string Reason { get; private set; }
        
        public bool IsValid(Position from, Position to, Field field)
        {
            if (!Utils.IsStraightLine(from, to) && !Utils.IsDiagonal(from, to))
            {
                Reason = "Invalid movement direction";
                return false;
            }
            var pawns = field.PawnsOnLine(from, to);
            if (pawns.Count > 0 || to.Pawn != null)
            {
                Reason = "Pawns are in the way";
                return false;
            }
            return true;
        }
        
        public ICommand ToCommand(Position from, Position to, Field field)
        {
            return new MoveCommand(from, to);
        }
    }
}