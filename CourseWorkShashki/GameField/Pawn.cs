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
        
        public static bool CanMove(this Pawn pawn, Status status)
        {
            return status.ToString().Contains(pawn.Color.ToString());
        } 
    } 
}