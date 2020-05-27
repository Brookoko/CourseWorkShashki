namespace Checkers.PathFinding
{
    using System.Collections.Generic;
    using System.Linq;
    using GameField;

    public class BFS
    {
        private readonly Field field;
        private readonly Position from;
        private readonly Position to;
        
        public BFS(Position from, Position to, Field field)
        {
            this.field = field;
            this.from = from;
            this.to = to;
        }
        
        public Path FindPath()
        {
            var paths = new List<Path> {new Path(from)};
            while (!paths.Any(p => p.Has(to)) && paths.Count != 0)
            {
                var branches = paths.SelectMany(path => field.PossibleAttackPositions(path.Last(), path.TurnToDame || from.Pawn.IsDame)
                    .Where(pos => !path.Has(pos))
                    .Where(pos => pos.Pawn == null || path.Opponents.Contains(pos))
                    .Select(pos => new Branch(path, pos, field.PawnsOnLine(path.Last(), pos))))
                    .ToList();
                paths = branches.Where(branch => branch.IsValid(from.Pawn))
                    .Select(branch => branch.CreatePath(from.Pawn))
                    .ToList();
            }
            return paths
                .Where(path => path.Has(to))
                .OrderBy(path => path.Length).FirstOrDefault();
        }
    }
}