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
            this.opponent = opponent;
        }
        
        public bool IsValid(Pawn pawn)
        {
            return opponent.Count == 1 && opponent[0].Pawn.Color != pawn.Color;
        }
        
        public Path CreatePath()
        {
            var newPath = new Path(path.Positions, path.Opponents);
            newPath.Positions.Add(next);
            newPath.Opponents.Add(opponent.First());
            return newPath;
        }

        public override string ToString()
        {
            return $"{path.First()} {next} {opponent.FirstOrDefault()}";
        }
    }
}