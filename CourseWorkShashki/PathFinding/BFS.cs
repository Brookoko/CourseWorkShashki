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
                    .Select(pos => new Branch(path, pos, PawnsOnLine(path.First(), pos))))
                    .ToList();
                paths = branches.Where(branch => branch.IsValid(from.Pawn))
                    .Select(branch => branch.CreatePath())
                    .ToList();
            }
            return paths
                .Where(path => path.Has(from))
                .OrderBy(path => path.Length).FirstOrDefault();
        }
        
        private List<Position> PawnsOnLine(Position from, Position to)
        {
            var x = from.X;
            var y = from.Y;
            var dx = to.X == from.X ? 0 : (to.X - from.X) / Math.Abs(to.X - from.X);
            var dy = to.Y == from.Y ? 0 : (to.Y - from.Y) / Math.Abs(to.Y - from.Y);
            var pawns = new List<Position>();
            while (x != to.X || y != to.Y)
            {
                x += dx;
                y += dy;
                var pos = field.GetPosition(x, y);
                if (pos != null && pos.Pawn != null && pos != to) pawns.Add(pos);
            }
            return pawns;
        }
    }
}