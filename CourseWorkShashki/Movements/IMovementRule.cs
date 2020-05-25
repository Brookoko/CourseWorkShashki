namespace Checkers.Movements
{
    using Commands;
    
    public interface IMovementRule
    {
        string Reason { get; }
        
        bool IsValid();
        
        ICommand ToCommand();
    }
}