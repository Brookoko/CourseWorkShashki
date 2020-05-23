namespace Movements
{
    using Checkers.PathFinding;
    using Commands;
    using GameField;
    
    public class FightRule : IMovementRule
    {
        public string Reason { get; private set; }
        
        private readonly Position from;
        private readonly Position to;
        private readonly Field field;
        
        public FightRule(Position from, Position to, Field field)
        {
            this.from = from;
            this.to = to;
            this.field = field;
        }
        
        public bool IsValid()
        {
            if (new DFS(from, to, field).FindPath() == null)
            {
                Reason = "No opponent pawn in the way";
                return false;
            }
            return true;
        }
        
        public ICommand ToCommand()
        {
            return new FightCommand(from, to, new DFS(from, to, field).FindPath());
        }
    }
}