namespace Commands
{
    using System.Collections.Generic;
    using Checkers.GameStatus;
    using DependencyInjection;
    using GameField;

    public interface ICommandQueue
    {
        void Execute(ICommand command);
        
        void Undo();
    }
    
    public class CommandQueue : ICommandQueue
    {
        [Inject]
        public IGameStatusProvider GameStatusProvider { get; set; }
        
        private readonly Stack<ICommand> stacks = new Stack<ICommand>();
        
        public void Execute(ICommand command)
        {
            command.Execute();
            stacks.Push(command);
            GameStatusProvider.GoToNext();
        }
        
        public void Undo()
        {
            if (stacks.Count == 0) return;
            var last = stacks.Pop();
            last.Undo();
        }
    }
}