namespace Movements
{
    using System;
    using Commands;
    using GameField;
    
    public class FightRule : IMovementRule
    {
        public string Name => "Fight";
        
        public bool IsValid(Position from, Position to, Field field, out string reason)
        {
            if (Math.Abs(to.X - from.X) != 2 && Math.Abs(to.Y - from.Y) != 2)
            {
                reason = "Invalid target";
                return false;
            }
            if (PawnOnLine(from, to, field) == null)
            {
                reason = "No opponent pawns in the way";
                return false;
            }
            reason = "Valid Move";
            return true;
        }
        
        public ICommand ToCommand(Position from, Position to, Field field)
        {
            return new FightCommand(from, to, PawnOnLine(from, to, field));
        }

        private Position PawnOnLine(Position from, Position to, Field field)
        {
            var x = from.X;
            var y = from.Y;
            var dx = to.X == from.X ? 0 : (to.X - from.X) / Math.Abs(to.X - from.X);
            var dy = to.Y == from.Y ? 0 : (to.Y - from.Y) / Math.Abs(to.Y - from.Y);
            while (x != to.X && y != to.Y)
            {
                x += dx;
                y += dy;
                var pos = field.Positions[x, y];
                if (pos.Pawn != null && pos.Pawn.Color != from.Pawn.Color) return pos;
            }
            return null;
        }
    }
}