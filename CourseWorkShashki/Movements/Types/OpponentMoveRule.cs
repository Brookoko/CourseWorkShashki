namespace Movements
{
    using Commands;
    
    public class OpponentMoveRule : IMovementRule
    {
        public string Reason => "Pawns cannot be moved during opponent turn";
        
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