namespace Commands
{
    using GameField;

    public class FightCommand : ICommand
    {
        private readonly Position from;
        private readonly Position to;
        private readonly Position kill;
        private readonly Pawn pawn;
        
        public FightCommand(Position from, Position to, Position kill)
        {
            this.from = from;
            this.to = to;
            this.kill = kill;
            pawn = kill.Pawn;
        }
        
        public void Execute()
        {
            to.Pawn = from.RemovePawn();
            kill.Pawn = null;
        }
        
        public void Undo()
        {
            from.Pawn = to.RemovePawn();
            kill.Pawn = pawn;
        }
    }
}