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
                case Status.WhiteWin:
                case Status.WhiteMove: return Color.White;
                case Status.BlackAttack:
                case Status.BlackWin:
                case Status.BlackMove: return Color.Black;
                default: return Color.White;
            }
        }
        
        public static bool IsMoving(this Status status)
        {
            return status == Status.WhiteMove || status == Status.BlackMove;
        }
        
        public static bool IsAttacking(this Status status)
        {
            return status == Status.WhiteAttack || status == Status.BlackAttack;
        }
        
        public static bool IsWinStatus(this Status status)
        {
            return status == Status.WhiteWin || status == Status.BlackWin;
        }
    }
}