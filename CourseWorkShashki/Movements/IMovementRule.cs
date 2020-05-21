namespace Movements
{
    using Commands;
    using GameField;

    public interface IMovementRule
    {
        string Name { get; }
        
        bool IsValid(Position from, Position to, Field field, out string reason);
        
        ICommand ToCommand(Position from, Position to, Field field);
    }
}