namespace Checkers.Movements
{
    public class OccupiedRule : IMovementRule
    {
        public IMovementRule Successor { get; set; }
        
        public bool IsValid(Move move)
        {
            if (move.To.Pawn != null)
            {
                move.RejectionReason = "Target position is occupied";
                return false;
            }
            return Successor?.IsValid(move) ?? true;
        }
    }
}