namespace GameField
{
    public class Field
    {
        public readonly Position[,] Positions = new Position[8, 8];

        public Field()
        {
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    Positions[i, j] = new Position(i, j);
                }
            }
        }
    }
}