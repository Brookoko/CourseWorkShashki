namespace Checkers.Movements
{
    public class NoPawnRule : IMovementRule
    {
        public IMovementRule Successor { get; set; }
        
        public bool IsValid(Move move)
        {
            if (move.From.Pawn == null)
            {
                move.RejectionReason = "No pawn is selected";
                return false;
            }
            return Successor?.IsValid(move) ?? true;
        }
    }
}