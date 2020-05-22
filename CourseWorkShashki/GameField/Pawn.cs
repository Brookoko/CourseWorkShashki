namespace GameField
{
    using Checkers.GameStatus;

    public class Pawn
    {
        public Color Color { get; }
        
        public bool IsDame { get; set; }
        
        public Pawn(Color color)
        {
            Color = color;
        }
    }
    
    public enum Color
    {
        Black,
        White
    }

    public static class PawnExtension
    {
        public static int ToDirection(this Color color) => color == Color.Black ? 1 : -1;
        
        public static Color Oppose(this Color color) => color == Color.Black ? Color.White : Color.Black;
        
        public static bool CanMove(this Pawn pawn, Status status)
        {
            return pawn.Color == status.ToColor();
        }
        
        public static Color ToColor(this Status status)
        {
            switch (status)
            {
                case Status.WhiteAttack:
                case Status.WhiteMove: return Color.White;
                case Status.BlackAttack:
                case Status.BlackMove: return Color.Black;
                default: return Color.White;
            }
        }
    } 
}