namespace Checkers.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using GameStatus;
    using PathFinding;
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
        private readonly Path path;
        private readonly List<Pawn> pawns;
        
        private bool turnedToDame;
        
        public FightCommand(Path path)
        {
            from = path.First();
            to = path.Last();
            this.path = path;
            pawns = path.Opponents.Select(p => p.Pawn).ToList();
        }
        
        public void Execute()
        {
            to.Pawn = from.RemovePawn();
            turnedToDame = to.TryTurnToDame();
            foreach (var opponent in path.Opponents)
            {
                opponent.Pawn = null;
            }
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
            for (var i = 0; i < path.Opponents.Count; i++)
            {
                path.Opponents[i].Pawn = pawns[i];
            }
        }
    }
}