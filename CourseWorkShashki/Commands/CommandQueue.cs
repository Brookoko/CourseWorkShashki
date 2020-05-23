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

        [Inject]
        public IInjectionBinder InjectionBinder { get; set; }
        
        private readonly Stack<ICommand> stacks = new Stack<ICommand>();
        
        public void Execute(ICommand command)
        {
            InjectionBinder.Inject(command);
            command.Execute();
            stacks.Push(command);
        }
        
        public void Undo()
        {
            if (stacks.Count == 0) return;
            var last = stacks.Pop();
            last.Undo();
        }
    }
}