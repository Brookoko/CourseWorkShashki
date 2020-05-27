namespace Checkers.Movements
{
    public class GameIsOverRule : IMovementRule
    {
        public IMovementRule Successor { get; set; }
        
        public bool IsValid(Move move)
        {
            move.RejectionReason = "Game is over";
            return false;
        }
    }
}