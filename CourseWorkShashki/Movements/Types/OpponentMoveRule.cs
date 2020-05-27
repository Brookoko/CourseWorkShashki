namespace Checkers.Movements
{
    public class OpponentMoveRule : IMovementRule
    {
        public IMovementRule Successor { get; set; }
        
        public bool IsValid(Move move)
        {
            if (move.From.Pawn.Color != move.Status.ToColor())
            {
                move.RejectionReason = "Pawns cannot be moved during opponent turn";
                return false;
            }
            return Successor?.IsValid(move) ?? true;
        }
    }
}