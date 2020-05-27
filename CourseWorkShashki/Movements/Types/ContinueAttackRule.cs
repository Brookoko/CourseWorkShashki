namespace Checkers.Movements
{
    using GameField;
    using GameStatus;

    public class ContinueAttackRule : IMovementRule
    {
        public IMovementRule Successor { get; set; }

        private Pawn lastAttacked;
        private Status status;
        
        public bool IsValid(Move move)
        {
            if (move.Status == status && move.From.Pawn != lastAttacked)
            {
                move.RejectionReason = "You should continue attack";
                return false;
            }
            if (move.Status.IsAttacking())
            {
                lastAttacked = move.From.Pawn;
                status = move.Status;
            }
            else
            {
                lastAttacked = null;
                status = Status.Menu;
            }
            return Successor?.IsValid(move) ?? true;
        }
    }
}