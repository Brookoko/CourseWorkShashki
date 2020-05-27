namespace Checkers.GameField
{
    public class Position
    {
        public Pawn Pawn { get; set; }
        
        public readonly int X;
        public readonly int Y;
        
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public Pawn RemovePawn()
        {
            var pawn = Pawn;
            Pawn = null;
            return pawn;
        }
        
        public bool TryTurnToDame()
        {
            if (TryTurnToDame(Pawn)) return Pawn.IsDame = true;
            return false;
        }
        
        public bool TryTurnToDame(Pawn pawn)
        {
            if (!pawn.IsDame && pawn.Color == Color.White && X == 0) return true;
            if (!pawn.IsDame && pawn.Color == Color.Black && X == 7) return true;
            return false;
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Position) obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }
        
        private bool Equals(Position other) => X == other.X && Y == other.Y;
        
        public static bool operator ==(Position left, Position right) => Equals(left, right);
        
        public static bool operator !=(Position left, Position right) => !Equals(left, right);
        
        public override string ToString() => $"{X} {Y}";
    }
}