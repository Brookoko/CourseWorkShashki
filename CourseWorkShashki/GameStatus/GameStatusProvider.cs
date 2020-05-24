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
            if (FieldProvider.Field.IsInWinState(color)) return ToWin(color);
            color = color.Oppose();
            return FieldProvider.Field.IsInAttackingState(color) ? ToAttack(color) : ToMove(color);
        }
        
        private Status ToWin(Color color) => color == Color.White ? Status.WhiteWin : Status.BlackWin;
        
        private Status ToAttack(Color color) => color == Color.White ? Status.WhiteAttack : Status.BlackAttack;
        
        private Status ToMove(Color color) => color == Color.White ? Status.WhiteMove : Status.BlackMove;
    }
}