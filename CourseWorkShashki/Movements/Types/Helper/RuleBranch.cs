namespace Checkers.Movements.Helper
{
    using System;

    public class RuleBranch
    {
        public Func<Move, bool> IsValid { get; set; }
        
        public IMovementRule Rule { get; set; }
    }
}