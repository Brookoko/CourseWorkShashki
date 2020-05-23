namespace Checkers.PathFinding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GameField;

    public class DFS
    {
        private readonly Field field;
        private readonly Position from;
        private readonly Position to;
        
        public DFS(Field field, Position from, Position to)
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
                var branches = paths.SelectMany(path => PossibleAttackPositions(path.First())
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
        
        private IEnumerable<Position> PossibleAttackPositions(Position position)
        {
            return new[]
                {
                    field.GetPosition(position.X + 2, position.Y + 2),
                    field.GetPosition(position.X + 2, position.Y - 2),
                    field.GetPosition(position.X - 2, position.Y + 2),
                    field.GetPosition(position.X - 2, position.Y - 2)
                }
                .Where(p => p != null);
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