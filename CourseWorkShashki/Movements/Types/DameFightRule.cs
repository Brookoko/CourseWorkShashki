namespace Movements
{
    using Checkers;
    using Commands;
    using GameField;

    public class DameFightRule : IMovementRule
    {
        public string Reason { get; private set; }
        
        private readonly Position from;
        private readonly Position to;
        private readonly Field field;
        
        public DameFightRule(Position from, Position to, Field field)
        {
            this.from = from;
            this.to = to;
            this.field = field;
        }
        
        public bool IsValid()
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
        
        public ICommand ToCommand()
        {
            return new FightCommand(from, to, null);
        }
    }
}