namespace Checkers.Movements
{
    using Commands;
    
    public class NoPawnRule : IMovementRule
    {
        public string Reason => "No pawn is selected";
        
        public bool IsValid()
        {
            return false;
        }
        
        public ICommand ToCommand()
        {
            return null;
        }
    }
}