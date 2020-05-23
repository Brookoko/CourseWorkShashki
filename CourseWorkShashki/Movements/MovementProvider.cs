namespace Movements
{
    using Checkers;
    using Checkers.GameStatus;
    using DependencyInjection;
    using GameField;
    
    public interface IMovementProvider
    {
        IMovementRule RuleFor(Position from, Position to, out string reason);
    }
    
    public class MovementProvider : IMovementProvider
    {
        [Inject]
        public IGameStatusProvider GameStatusProvider { get; set; }
        
        [Inject]
        public IFieldProvider FieldProvider { get; set; }
        
        private IMovementRule simpleMove = new SimpleMovementRule();
        private IMovementRule simpleFight = new SimpleFightRule();
        private IMovementRule dameMove = new DameMovementRule();
        private IMovementRule dameFight = new DameFightRule();
        
        public IMovementRule RuleFor(Position from, Position to, out string reason)
        {
            if (from.Pawn == null)
            {
                reason = "No pawn at start position";
                return null;
            }
            if (!from.Pawn.CanMove(GameStatusProvider.Status))
            {
                reason = "Wrong pawn move";
                return null;
            }
            
            return from.Pawn.IsDame ?
                GetDameRule(from, to, FieldProvider.Field, out reason) :
                GetSimpleRule(from, to, FieldProvider.Field, out reason);
        }
        
        private IMovementRule GetSimpleRule(Position from, Position to, Field field, out string rejection)
        {
            if (IsInAttackingState(field))
            {
                return dameFight.IsValid(from, to, field, out rejection) ? dameFight : null;
            }
            return dameMove.IsValid(from, to, field, out rejection) ? dameMove : null;
        }
        
        private IMovementRule GetDameRule(Position from, Position to, Field field, out string rejection)
        {
            if (IsInAttackingState(field))
            {
                return simpleFight.IsValid(from, to, field, out rejection) ? simpleFight : null;
            }
            return simpleMove.IsValid(from, to, field, out rejection) ? simpleMove : null;
        }
        
        private bool IsInAttackingState(Field field)
        {
            return field.IsInAttackingState(GameStatusProvider.Status.ToColor());
        }
    }
}