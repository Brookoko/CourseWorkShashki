namespace Checkers.Commands
{
    using System;
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
        private readonly bool turnToDame;
        
        private Status lastStatus;
        
        public FightCommand(Path path)
        {
            this.path = path;
            from = path.First();
            to = path.Last();
            turnToDame = path.TurnToDame;
            pawns = path.Opponents.Select(p => p.Pawn).ToList();
        }
        
        public void Execute()
        {
            if (turnToDame) from.Pawn.IsDame = true;
            to.Pawn = from.RemovePawn();
            foreach (var opponent in path.Opponents)
            {
                Console.WriteLine($"Opponent: {opponent}");
                opponent.Pawn = null;
            }
            lastStatus = GameStatusProvider.Status;
            GoToNext();
        }
        
        private void GoToNext()
        {
            if (FieldProvider.Field.IsInAttackingState(to)) return;
            GameStatusProvider.GoToNext();
        }
        
        public void Undo()
        {
            if (turnToDame) to.Pawn.IsDame = false;
            from.Pawn = to.RemovePawn();
            for (var i = 0; i < path.Opponents.Count; i++)
            {
                path.Opponents[i].Pawn = pawns[i];
            }
            GameStatusProvider.UpdateStatus(lastStatus);
        }
    }
}