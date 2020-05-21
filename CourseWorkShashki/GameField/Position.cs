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
    }
}