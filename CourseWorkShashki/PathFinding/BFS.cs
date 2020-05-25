namespace Checkers.PathFinding
{
    using System;
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
            var paths = new List<Path> {new Path(to)};
            while (!paths.Any(p => p.Has(from)) && paths.Count != 0)
            {
                var branches = paths.SelectMany(path => field.PossibleAttackPositions(path.First(), from.Pawn.IsDame)
                    .Where(pos => !path.Has(pos))
                    .Where(pos => pos.Pawn == null || pos == from)
                    .Select(pos => new Branch(path, pos, field.PawnsOnLine(path.First(), pos))))
                    .ToList();
                paths = branches.Where(branch => branch.IsValid(from.Pawn))
                    .Select(branch => branch.CreatePath())
                    .ToList();
            }
            return paths
                .Where(path => path.Has(from))
                .OrderBy(path => path.Length).FirstOrDefault();
        }
    }
}