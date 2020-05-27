namespace Checkers.PathFinding
{
    using System.Collections.Generic;
    using System.Linq;
    using GameField;

    public class Branch
    {
        private readonly Path path;
        private readonly Position next;
        private readonly List<Position> opponent;
        
        public Branch(Path path, Position next, List<Position> opponent)
        {
            this.path = path;
            this.next = next;
            opponent.RemoveAll(op => path.Opponents.Contains(op));
            this.opponent = opponent;
        }
        
        public bool IsValid(Pawn pawn)
        {
            return opponent.Count == 1 && opponent[0].Pawn.Color != pawn.Color;
        }
        
        public Path CreatePath(Pawn pawn)
        {
            var newPath = new Path(path.Positions, path.Opponents);
            newPath.Positions.Add(next);
            newPath.Opponents.Add(opponent.First());
            newPath.TurnToDame = next.TryTurnToDame(pawn) || path.TurnToDame;
            return newPath;
        }
    }
}