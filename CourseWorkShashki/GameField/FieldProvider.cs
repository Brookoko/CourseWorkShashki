namespace GameField
{
    public interface IFieldProvider
    {
        Field Field { get; }
        
        void CreateNew();
    }
    
    public class FieldProvider : IFieldProvider
    {
        public Field Field { get; private set; }
        
        public void CreateNew()
        {
            Field = new Field();
        }
    }
}