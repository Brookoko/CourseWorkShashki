namespace Checkers.Movements.Helper
{
    using System.Linq;

    public class BranchingRule : IMovementRule
    {
        public IMovementRule Successor { get; set; }

        private RuleBranch[] branches;
        
        public BranchingRule(params RuleBranch[] branches)
        {
            this.branches = branches;
        }
        
        public bool IsValid(Move move)
        {
            var branch = branches.FirstOrDefault(b => b.IsValid(move));
            if (branch == null) return Successor?.IsValid(move) ?? false;
            return branch.Rule.IsValid(move);
        }
    }
}