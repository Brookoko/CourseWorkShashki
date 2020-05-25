namespace GameField
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Checkers;

    public class Field
    {
        public readonly Position[,] Positions = new Position[8, 8];

        public readonly Pawn[] Checkers = new Pawn[24];
        
        public Position GetPosition(int x, int y)
        {
            if (x >= 0 && x <= 7 && y >= 0 && y <= 7)
            {
                return Positions[x, y];
            }
            return null;
        }
        
        public List<Position> PawnsOnLine(Position from, Position to)
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
                var pos = GetPosition(x, y);
                if (pos != null && pos.Pawn != null && pos != to) pawns.Add(pos);
            }
            return pawns;
        }
        
        public bool IsInWinState(Color color)
        {
            var oppose = color.Oppose();
            return Positions.Cast<Position>()
                .Select(p => p.Pawn)
                .Where(p => p != null)
                .All(p => p.Color != oppose);
        }
        
        public bool IsInAttackingState(Color color)
        {
            return Positions.Cast<Position>()
                .Any(position => position.Pawn != null &&
                                 position.Pawn.Color == color &&
                                 IsInAttackingState(position));
        }
        
        public bool IsInAttackingState(Position position)
        {
            if (position.Pawn == null) return false;
            return PossibleAttackPositions(position, position.Pawn.IsDame)
                .Where(pos => pos.Pawn == null)
                .Any(pos => IsOpponentPawnOnLine(position, pos));
        }

        public IEnumerable<Position> PossibleAttackPositions(Position position, bool isDame)
        {
            return isDame ? GetDameAttackPositions(position) : GetSimpleAttackPositions(position);
        }
        
        private IEnumerable<Position> GetSimpleAttackPositions(Position position)
        {
            return new[]
                {
                    GetPosition(position.X + 2, position.Y + 2),
                    GetPosition(position.X + 2, position.Y - 2),
                    GetPosition(position.X - 2, position.Y + 2),
                    GetPosition(position.X - 2, position.Y - 2)
                }
                .Where(p => p != null);
        }
        
        private IEnumerable<Position> GetDameAttackPositions(Position position)
        {
            var positions = new List<Position>();
            for (var i = 1; i < 8; i++)
            {
                positions.Add(GetPosition(position.X + i, position.Y));
                positions.Add(GetPosition(position.X - i, position.Y));
                positions.Add(GetPosition(position.X, position.Y + i));
                positions.Add(GetPosition(position.X, position.Y - i));
                positions.Add(GetPosition(position.X + i, position.Y + i));
                positions.Add(GetPosition(position.X + i, position.Y - i));
                positions.Add(GetPosition(position.X - i, position.Y + i));
                positions.Add(GetPosition(position.X - i, position.Y - i));
            }
            return positions.Where(p => p != null);
        }
        
        private bool IsOpponentPawnOnLine(Position from, Position to)
        {
            var pawns = PawnsOnLine(from, to);
            return pawns.Count == 1 && pawns[0].Pawn.Color != from.Pawn.Color;
        }
    }
}