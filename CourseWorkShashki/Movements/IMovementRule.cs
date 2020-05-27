namespace Checkers.Movements
{
    public interface IMovementRule
    {
        IMovementRule Successor { get; set; }
        
        bool IsValid(Move move);
    }
}