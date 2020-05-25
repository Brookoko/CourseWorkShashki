namespace Checkers.Movements
{
    using Checkers;
    using GameStatus;
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
            if (to.Pawn != null) return new OccupiedRule();
            
            return from.Pawn.IsDame ? GetDameRule(from, to, field) : GetSimpleRule(from, to, field);
        }
        
        private IMovementRule GetSimpleRule(Position from, Position to, Field field)
        {
            if (GameStatusProvider.Status.IsAttacking()) return new FightRule(from, to, field);
            return new SimpleMovementRule(from, to);
        }
        
        private IMovementRule GetDameRule(Position from, Position to, Field field)
        {
            if (GameStatusProvider.Status.IsAttacking()) return new FightRule(from, to, field);
            return new DameMovementRule(from, to, field);
        }
    }
}