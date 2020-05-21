namespace GameField
{
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

    public static class ColorExtension
    {
        public static int ToDirection(this Color color) => color == Color.Black ? -1 : 1;
    } 
}