namespace Movements
{
    using Commands;
    using GameField;

    public interface IMovementRule
    {
        bool IsValid(Position from, Position to, out string reason);
        
        ICommand ToCommand(Position from, Position to);
    }
}