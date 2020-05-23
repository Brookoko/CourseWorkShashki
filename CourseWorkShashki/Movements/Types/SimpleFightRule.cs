namespace Movements
{
    using Checkers.PathFinding;
    using Commands;
    using GameField;
    
    public class SimpleFightRule : IMovementRule
    {
        public string Reason { get; private set; }
        
        public bool IsValid(Position from, Position to, Field field)
        {
            if (to.Pawn != null || from == to)
            {
                Reason = "Invalid target position";
                return false;
            }
            if (new DFS(field, from, to).FindPath() == null)
            {
                Reason = "No opponent pawn in the way";
                return false;
            }
            return true;
        }
        
        public ICommand ToCommand(Position from, Position to, Field field)
        {
            return new FightCommand(from, to, new DFS(field, from, to).FindPath());
        }
    }
}