namespace GameField
{
    public class Position
    {
        public Checker Checker { get; set; }
        
        public readonly int x;
        public readonly int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}