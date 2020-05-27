namespace Checkers.Movements
{
    using System;
    using PathFinding;
    
    public class FightRule : IMovementRule
    {
        public IMovementRule Successor { get; set; }
        
        public bool IsValid(Move move)
        {
            if (new BFS(move.From, move.To, move.Field).FindPath() == null)
            {
                move.RejectionReason = "No opponent pawn in the way";
                return false;
            }
            return Successor?.IsValid(move) ?? true;
        }
    }
}