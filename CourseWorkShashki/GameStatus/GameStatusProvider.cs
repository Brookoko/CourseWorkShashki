namespace Checkers.GameStatus
{
    public interface IGameStatusProvider
    {
        Status Status { get; }

        void UpdateStatus(Status status);
    }
    
    public class GameStatusProvider : IGameStatusProvider
    {
        public Status Status { get; private set; }
        
        public void UpdateStatus(Status status)
        {
            Status = status;
        }
    }
}