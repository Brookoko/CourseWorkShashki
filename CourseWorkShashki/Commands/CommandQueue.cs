namespace Commands
{
    using System.Collections.Generic;
    
    public interface ICommandQueue
    {
        void Execute(ICommand command);
        
        void Undo();
    }
    
    public class CommandQueue : ICommandQueue
    {
        private readonly Stack<ICommand> stacks = new Stack<ICommand>();
        
        public void Execute(ICommand command)
        {
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