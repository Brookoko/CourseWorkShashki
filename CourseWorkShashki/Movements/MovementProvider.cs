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
            
            return from.Pawn.IsDame ? GetDameRule(field) : GetSimpleRule(field);
        }
        
        private IMovementRule GetSimpleRule(Field field)
        {
            if (IsInAttackingState(field)) return new SimpleFightRule();
            return new SimpleMovementRule();
        }
        
        private IMovementRule GetDameRule(Field field)
        {
            if (IsInAttackingState(field)) return new DameFightRule();
            return new DameMovementRule();
        }
        
        private bool IsInAttackingState(Field field)
        {
            return field.IsInAttackingState(GameStatusProvider.Status.ToColor());
        }
    }
}