namespace Checkers.Movements
{
    using GameField;
    using GameStatus;
    
    public class Move
    {
        public Position From { get; set; }
        
        public Position To { get; set; }
        
        public Field Field { get; set; }
        
        public Status Status { get; set; }
        
        public string RejectionReason { get; set; }
    }
}