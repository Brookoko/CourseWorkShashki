namespace Checkers
{
    using System;
    using GameField;

    public static class Utils
    {
        public static bool IsDiagonalInDirection(Position from, Position to, int direction)
        {
            return Math.Abs(to.X - from.X) == Math.Abs(to.Y - from.Y) && to.X - from.X == direction;
        }
     
        public static bool IsDiagonal(Position from, Position to, int distance)
        {
            return Math.Abs(to.X - from.X) == Math.Abs(to.Y - from.Y) && Math.Abs(to.X - from.X) == distance;
        }
        
        public static bool IsDiagonal(Position from, Position to)
        {
            return Math.Abs(to.X - from.X) == Math.Abs(to.Y - from.Y);
        }

        public static bool IsStraightLine(Position from, Position to)
        {
            return from.X == to.X && from.Y != to.Y || from.Y == to.Y && from.X != to.X;
        }
    }
}