namespace Movements
{
    using Commands;
    using GameField;

    public interface IMovementRule
    {
        string Reason { get; }
        
        bool IsValid(Position from, Position to, Field field);
        
        ICommand ToCommand(Position from, Position to, Field field);
    }
}