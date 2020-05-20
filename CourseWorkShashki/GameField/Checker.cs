namespace GameField
{
    public class Checker
    {
        public Color Color { get; }
        
        public bool IsDame { get; set; }
        
        public Checker(Color color)
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