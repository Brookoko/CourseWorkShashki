namespace Checkers.Movements
{
    using Checkers;
    
    public class DameMovementRule : IMovementRule
    {
        public IMovementRule Successor { get; set; }
        
        public bool IsValid(Move move)
        {
            if (!Utils.IsDiagonal(move.From, move.To))
            {
                move.RejectionReason = "Invalid movement direction";
                return false;
            }
            var pawns = move.Field.PawnsOnLine(move.From, move.To);
            if (pawns.Count != 0)
            {
                move.RejectionReason = "Pawns are in the way";
                return false;
            }
            return Successor?.IsValid(move) ?? true;
        }
    }
}