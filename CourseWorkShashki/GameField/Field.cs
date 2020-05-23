namespace GameField
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Field
    {
        public readonly Position[,] Positions = new Position[8, 8];

        private readonly Pawn[] checkers = new Pawn[24];
        
        public Field()
        {
            for (var i = 0; i < 24; i++)
            {
                checkers[i] = new Pawn((Color) (i / 12));
            }
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    Positions[i, j] = new Position(i, j);
                }
            }
            for (var k = 0; k < 24; k += 2)
            {
                var i = k / 8;
                var j = k - i * 8 + i % 2;
                Positions[i, j].Pawn = checkers[k / 2];
            }
            for (var k = 0; k < 24; k += 2)
            {
                var i = 7 - k / 8;
                var j = 63 - k - i * 8 + (i % 2 - 1);
                Positions[i, j].Pawn = checkers[k / 2 + 12];
            }
        }
        
        public Position GetPosition(int x, int y)
        {
            if (x >= 0 && x <= 7 && y >= 0 && y <= 7)
            {
                return Positions[x, y];
            }
            return null;
        }
        
        public Position OpponentPawnOnLine(Position from, Position to)
        {
            return PawnsOnLine(from, to).FirstOrDefault(p => p.Pawn.Color != from.Pawn.Color);
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
                var pos = Positions[x, y];
                if (pos.Pawn != null && pos != to) pawns.Add(pos);
            }
            return pawns;
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
            return PossibleAttackPositions(position)
                .Any(pos => pos.Pawn == null && OpponentPawnOnLine(position, pos) != null);
        }
        
        private IEnumerable<Position> PossibleAttackPositions(Position position)
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
    }
}