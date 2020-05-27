namespace Checkers.Movements
{
    using Checkers;
    using Commands;
    using DependencyInjection;
    using Helper;
    using PathFinding;
    
    public interface IMovementProvider
    {
        bool IsValid(Move move);

        ICommand ToCommand(Move move);
    }
    
    public class MovementProvider : IMovementProvider
    {
        private IMovementRule rule;
        
        [PostConstruct]
        public void Prepare()
        {
            var noPawn = new NoPawnRule();
            var opponent = new OpponentMoveRule();
            var occupied = new OccupiedRule();
            
            var attackBranch = new RuleBranch
            {
                IsValid = move => move.Status.IsAttacking(),
                Rule = new FightRule()
            };
            
            var dameBranch = new RuleBranch
            {
                IsValid = move => move.From.Pawn.IsDame,
                Rule = new DameMovementRule()
            };
            
            var moveBranch = new RuleBranch
            {
                IsValid = move => move.Status.IsMoving(),
                Rule = new BranchingRule(dameBranch) {Successor = new SimpleMovementRule()}
            };
            
            var branching = new BranchingRule(attackBranch, moveBranch) {Successor = new GameIsOverRule()};

            noPawn.Successor = opponent;
            opponent.Successor = occupied;
            occupied.Successor = branching;
            rule = noPawn;
        }
        
        public bool IsValid(Move move)
        {
            return rule.IsValid(move);
        }
        
        public ICommand ToCommand(Move move)
        {
            if (move.Status.IsAttacking()) return new FightCommand(new BFS(move.From, move.To, move.Field).FindPath());
            return new MoveCommand(move.From, move.To);
        }
    }
}