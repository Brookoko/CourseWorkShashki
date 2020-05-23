namespace Checkers.GameStatus
{
    using DependencyInjection;
    using GameField;
    
    public interface IGameStatusProvider
    {
        Status Status { get; }

        void UpdateStatus(Status status);
        
        void GoToNext();
    }
    
    public class GameStatusProvider : IGameStatusProvider
    {
        [Inject]
        public IFieldProvider FieldProvider { get; set; }
        
        public Status Status { get; private set; }
        
        public void UpdateStatus(Status status)
        {
            Status = status;
        }
        
        public void GoToNext()
        {
            UpdateStatus(StatusFor(Status.ToColor()));
        }
        
        private Status StatusFor(Color color)
        {
            if (FieldProvider.Field.IsInWinState(color))
                return color == Color.White ? Status.WhiteWin : Status.BlackWin;
            color = color.Oppose();
            if (FieldProvider.Field.IsInAttackingState(color))
                return color == Color.White ? Status.WhiteAttack : Status.BlackAttack;
            return color == Color.White ? Status.WhiteMove : Status.BlackMove;
        }
    }
}