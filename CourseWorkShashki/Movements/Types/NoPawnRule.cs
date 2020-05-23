namespace Movements
{
    using Commands;
    using GameField;

    public class NoPawnRule : IMovementRule
    {
        public string Reason => "No pawn is selected";
        
        public bool IsValid(Position from, Position to, Field field)
        {
            return false;
        }
        
        public ICommand ToCommand(Position from, Position to, Field field)
        {
            return null;
        }
    }
}