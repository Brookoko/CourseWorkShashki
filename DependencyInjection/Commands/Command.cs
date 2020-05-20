namespace DependencyInjection.Commands
{
    using System.Threading.Tasks;
    
    public abstract class Command
    {
        public bool IsFailed { get; private set; }
        
        public TaskCompletionSource<bool> ReleaseTask;
        
        public abstract void Execute();
        
        protected void Retain()
        {
            ReleaseTask = new TaskCompletionSource<bool>(false);
        }
        
        protected void Release()
        {
            ReleaseTask.SetResult(true);
        }
        
        protected void Fail()
        {
            IsFailed = true;
        }
    }
}