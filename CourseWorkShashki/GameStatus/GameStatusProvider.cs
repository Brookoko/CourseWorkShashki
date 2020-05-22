namespace Checkers.GameStatus
{
    public interface IGameStatusProvider
    {
        Status Status { get; }

        void UpdateStatus(Status status);
        
        void GoToNext();
    }
    
    public class GameStatusProvider : IGameStatusProvider
    {
        public Status Status { get; private set; }
        
        public void UpdateStatus(Status status)
        {
            Status = status;
        }
        
        public void GoToNext()
        {
            if (Status == Status.WhiteMove) UpdateStatus(Status.BlackMove);
            else if (Status == Status.BlackMove) UpdateStatus(Status.WhiteMove);
        }
    }
}