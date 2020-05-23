namespace Movements
{
    using Checkers;
    using Commands;
    using GameField;

    public class SimpleMovementRule : IMovementRule
    {
        public string Reason { get; private set; }
        
        public bool IsValid(Position from, Position to, Field field)
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
        
        public ICommand ToCommand(Position from, Position to, Field field)
        {
            return new MoveCommand(from, to);
        }
    }
}