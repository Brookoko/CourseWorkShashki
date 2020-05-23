namespace Movements
{
    using Commands;
    using GameField;

    public class OpponentMoveRule : IMovementRule
    {
        public string Reason => "Pawns cannot be moved during opponent turn";
        
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