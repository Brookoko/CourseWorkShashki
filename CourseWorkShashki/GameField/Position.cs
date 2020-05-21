namespace GameField
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
            if (Pawn.Color == Color.White && X == 0) return Pawn.IsDame = true;
            if (Pawn.Color == Color.Black && X == 7) return Pawn.IsDame = true;
            return false;
        }
    }
}