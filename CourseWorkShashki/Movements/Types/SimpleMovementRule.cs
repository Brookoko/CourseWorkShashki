namespace Movements
{
    using Checkers;
    using Commands;
    using GameField;

    public class SimpleMovementRule : IMovementRule
    {
        public string Reason { get; private set; }
        
        private readonly Position from;
        private readonly Position to;
        
        public SimpleMovementRule(Position from, Position to)
        {
            this.from = from;
            this.to = to;
        }
        
        public bool IsValid()
        {
            if (!Utils.IsDiagonalInDirection(from, to, from.Pawn.Color.ToDirection()))
            {
                Reason = "Invalid target position";;
                return false;
            }
            if (to.Pawn != null)
            {
                Reason = "Target position is occupied";
                return false;
            }
            return true;
        }
        
        public ICommand ToCommand()
        {
            return new MoveCommand(from, to);
        }
    }
}