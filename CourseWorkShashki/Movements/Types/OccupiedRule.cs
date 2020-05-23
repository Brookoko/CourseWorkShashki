namespace Movements
{
    using Commands;

    public class OccupiedRule : IMovementRule
    {
        public string Reason => "Target position is occupied";
        
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