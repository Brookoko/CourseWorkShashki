namespace Checkers.GameField
{
    public interface IFieldProvider
    {
        Field Field { get; }
        
        void CreateNew();
    }
    
    public class FieldProvider : IFieldProvider
    {
        public Field Field { get; set; }
        
        public void CreateNew()
        {
            Field = new Field();
            SetUpField();
        }

        private void SetUpField()
        {
            for (var i = 0; i < 24; i++)
            {
                Field.Checkers[i] = new Pawn((Color) (i / 12));
            }
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    Field.Positions[i, j] = new Position(i, j);
                }
            }
            for (var k = 0; k < 24; k += 2)
            {
                var i = k / 8;
                var j = k - i * 8 + i % 2;
                Field.Positions[i, j].Pawn = Field.Checkers[k / 2];
            }
            for (var k = 0; k < 24; k += 2)
            {
                var i = 7 - k / 8;
                var j = 63 - k - i * 8 + (i % 2 - 1);
                Field.Positions[i, j].Pawn = Field.Checkers[k / 2 + 12];
            }
        }
    }
}