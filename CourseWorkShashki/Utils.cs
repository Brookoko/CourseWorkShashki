namespace Checkers
{
    using System;
    using GameField;
    using GameStatus;

    public static class Utils
    {
        public static bool IsDiagonalInDirection(Position from, Position to, int direction)
        {
            return Math.Abs(to.X - from.X) == Math.Abs(to.Y - from.Y) && to.X - from.X == direction;
        }
     
        public static bool IsDiagonal(Position from, Position to)
        {
            return Math.Abs(to.X - from.X) == Math.Abs(to.Y - from.Y);
        }

        public static bool IsStraightLine(Position from, Position to)
        {
            return from.X == to.X && from.Y != to.Y || from.Y == to.Y && from.X != to.X;
        }
        
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
        
        public static string ToPrompt(this Status status)
        {
            switch (status)
            {
                case Status.Menu: return "Menu: ";
                case Status.WhiteMove: return "Move (white): ";
                case Status.BlackMove: return "Move (black): ";
                case Status.WhiteAttack: return "Attack (white): ";
                case Status.BlackAttack: return "Attack (black): ";
                case Status.WhiteWin: return "White win\n";
                case Status.BlackWin: return "Black win\n";
                default: return "";
            }
        }
    }
}