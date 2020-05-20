namespace GameField
{
    using System;

    public class Field
    {
        public readonly Position[,] Positions = new Position[8, 8];

        private readonly Checker[] checkers = new Checker[24];
        
        public Field()
        {
            for (var i = 0; i < 24; i++)
            {
                checkers[i] = new Checker((Color) (i / 12));
            }
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    Positions[i, j] = new Position(i, j);
                }
            }
            for (var k = 0; k < 24; k += 2)
            {
                var i = k / 8;
                var j = k - i * 8 + i % 2;
                Positions[i, j].Checker = checkers[k / 2];
            }
            for (var k = 0; k < 24; k += 2)
            {
                var i = 7 - k / 8;
                var j = 63 - k - i * 8 + (i % 2 - 1);
                Console.WriteLine($"{i} {j} {k/2}");
                Positions[i, j].Checker = checkers[k / 2 + 12];
            }
        }
    }
}