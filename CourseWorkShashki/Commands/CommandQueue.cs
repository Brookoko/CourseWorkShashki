namespace Checkers.Commands
{
    using System.Collections.Generic;
    using DependencyInjection;

    public interface ICommandQueue
    {
        void Execute(ICommand command);
        
        void Undo();
        
        void Reset();
    }
    
    public class CommandQueue : ICommandQueue
    {
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
        
        public void Reset()
        {
            stacks.Clear();
        }
    }
}