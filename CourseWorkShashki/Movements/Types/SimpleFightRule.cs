namespace Movements
{
    using Checkers.PathFinding;
    using Commands;
    using GameField;
    
    public class SimpleFightRule : IMovementRule
    {
        public string Reason { get; private set; }
        
        private readonly Position from;
        private readonly Position to;
        private readonly Field field;
        
        public SimpleFightRule(Position from, Position to, Field field)
        {
            this.from = from;
            this.to = to;
            this.field = field;
        }
        
        public bool IsValid()
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
        
        public ICommand ToCommand()
        {
            return new FightCommand(from, to, new DFS(field, from, to).FindPath());
        }
    }
}