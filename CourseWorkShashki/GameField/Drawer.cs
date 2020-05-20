namespace GameField
{
    using System;
    using System.Text;

    public class Drawer
    {
        private const int Width = 5;

        private readonly string[,] leftCorners = { {"┌", "┬"}, {"├", "┼"} };
        private readonly string[,] rightCorners = { {"┐", "┬"}, {"┤", "┼"} };
        private readonly string[] checkers = { "◎", "◉", " " };
        
        public void Draw(Field field)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Clear();
            Console.WriteLine("      0     1     2     3     4     5     6     7 ");
            for (var i = 0; i < 8; i++)
            {
                DrawUpperLine(field.Positions, i);
            }
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
                var checker = matrix[i, j].Checker;
                var index = checker == null ? 2 : (int) checker.Color;
                builder.Append(' ', Width/2)
                    .Append(checkers[index])
                    .Append(' ', Width/2)
                    .Append('│');
            }
            Console.WriteLine(builder.ToString());
        }
        
        private string Upper(Position pos, int width)
        {
            return new StringBuilder(ToLeftCorner(pos.x, pos.y))
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
    }
}