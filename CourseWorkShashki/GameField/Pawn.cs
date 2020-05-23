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
}