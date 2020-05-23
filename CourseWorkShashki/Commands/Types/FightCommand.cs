namespace Commands
{
    using Checkers.GameStatus;
    using DependencyInjection;
    using GameField;

    public class FightCommand : ICommand
    {
        [Inject]
        public IGameStatusProvider GameStatusProvider { get; set; }
        
        [Inject]
        public IFieldProvider FieldProvider { get; set; }
        
        private readonly Position from;
        private readonly Position to;
        private readonly Position kill;
        private readonly Pawn pawn;
        
        private bool turnedToDame;
        
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
            turnedToDame = to.TryTurnToDame();
            kill.Pawn = null;
            GoToNext();
        }
        
        private void GoToNext()
        {
            if (FieldProvider.Field.IsInAttackingState(to)) return;
            GameStatusProvider.GoToNext();
        }
        
        public void Undo()
        {
            GoToNext();
            from.Pawn = to.RemovePawn();
            if (turnedToDame) from.Pawn.IsDame = false;
            kill.Pawn = pawn;
        }
    }
}