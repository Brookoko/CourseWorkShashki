namespace Movements
{
    using Checkers;
    using Checkers.GameStatus;
    using DependencyInjection;
    using GameField;
    
    public interface IMovementProvider
    {
        IMovementRule RuleFor(Position from, Position to, Field field);
    }
    
    public class MovementProvider : IMovementProvider
    {
        [Inject]
        public IGameStatusProvider GameStatusProvider { get; set; }
        
        public IMovementRule RuleFor(Position from, Position to, Field field)
        {
            if (from.Pawn == null) return new NoPawnRule();
            if (!from.Pawn.CanMove(GameStatusProvider.Status)) return new OpponentMoveRule();
            
            return from.Pawn.IsDame ? GetDameRule(from, to, field) : GetSimpleRule(from, to, field);
        }
        
        private IMovementRule GetSimpleRule(Position from, Position to, Field field)
        {
            if (IsInAttackingState()) return new SimpleFightRule(from, to, field);
            return new SimpleMovementRule(from, to);
        }
        
        private IMovementRule GetDameRule(Position from, Position to, Field field)
        {
            if (IsInAttackingState()) return new DameFightRule(from, to, field);
            return new DameMovementRule(from, to, field);
        }
        
        private bool IsInAttackingState()
        {
            return GameStatusProvider.Status == Status.WhiteAttack || GameStatusProvider.Status == Status.BlackAttack;
        }
    }
}