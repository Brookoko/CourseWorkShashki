namespace Checkers.Movements
{
    using Checkers;
    
    public class SimpleMovementRule : IMovementRule
    {
        public IMovementRule Successor { get; set; }
        
        public bool IsValid(Move move)
        {
            if (!Utils.IsDiagonalInDirection(move.From, move.To, move.From.Pawn.Color.ToDirection()))
            {
                move.RejectionReason = "Invalid move direction";
                return false;
            }
            return Successor?.IsValid(move) ?? true;
        }
    }
}