namespace Movements
{
    using Checkers;
    using Commands;
    using GameField;

    public class DameFightRule : IMovementRule
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
            if (pawns.Count != 1 || to.Pawn != null)
            {
                Reason = "Invalid attack";
                return false;
            }
            return true;
        }
        
        public ICommand ToCommand(Position from, Position to, Field field)
        {
            return new FightCommand(from, to, null);
        }
    }
}