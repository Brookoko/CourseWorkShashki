namespace GameField
{
    using System;
    using System.Text;

    public class Drawer
    {
        private const int Width = 5;

        private readonly string[,] leftCorners = { {"┌", "┬"}, {"├", "┼"} };
        private readonly string[,] rightCorners = { {"┐", "┬"}, {"┤", "┼"} };
        private readonly string[] pawns = { "◎", "◉", "◯", "◍", " " };
        
        public void Draw(Field field)
        {
            Console.OutputEncoding = Encoding.UTF8;
            DrawUpperHeader();
            for (var i = 0; i < 8; i++)
            {
                DrawUpperLine(field.Positions, i);
            }
            DrawBottom();
        }
        
        private void DrawUpperHeader()
        {
            var upper = new StringBuilder()
                .Append(' ', 6)
                .Append(0);
            for (var i = 1; i < 8; i++)
            {
                upper.Append(' ', Width)
                    .Append(i);
            }
            Console.WriteLine(upper.ToString());
        }
        
        private void DrawUpperLine(Position[,] matrix, int i)
        {
            var builder = new StringBuilder("   ");
            for (var j = 0; j < 8; j++)
            {
                builder.Append(Upper(matrix[i, j], Width));
            }
            builder.Append(ToRightCorner(i, 7))
                .AppendLine()
                .Append(i)
                .Append(' ', 2)
                .Append('│');
            for (var j = 0; j < 8; j++)
            {
                var pawn = matrix[i, j].Pawn;
                var index = pawn == null ? 4 : (int) pawn.Color + (pawn.IsDame ? 2 : 0);
                builder.Append(' ', Width/2)
                    .Append(pawns[index])
                    .Append(' ', Width/2)
                    .Append('│');
            }
            Console.WriteLine(builder.ToString());
        }
        
        private string Upper(Position pos, int width)
        {
            return new StringBuilder(ToLeftCorner(pos.X, pos.Y))
                .Append('─', width)
                .ToString();
        }
        
        private string ToLeftCorner(int x, int y)
        {
            x = x == 0 ? 0 : 1;
            y = y == 0 ? 0 : 1;
            return leftCorners[x, y];
        }
        
        private string ToRightCorner(int x, int y)
        {
            x = x == 0 ? 0 : 1;
            y = y == 7 ? 0 : 1;
            return rightCorners[x, y];
        }
        
        private void DrawBottom()
        {
            var bottom = new StringBuilder()
                .Append(' ', 3)
                .Append('└');
            for (var i = 0; i < 7; i++)
            {
                bottom.Append('─', Width).Append('┴');
            }
            bottom.Append('─', Width).Append('┘');
            Console.WriteLine(bottom.ToString());
        }
    }
}