namespace AppContext
{
    using DependencyInjection;
    using DependencyInjection.Commands;
    using JetBrains.Annotations;

    public interface IModuleInstaller
    {
        IModuleInitializer LogicInitializer { get; }

        Priority Priority { get; }

        string Name { get; }

        void ExecuteAfterBindings(IInjectionBinder injector, ICommandInjector commandInjector);
    }

    [UsedImplicitly]
    public abstract class ModuleInstaller : IModuleInstaller
    {
        public virtual IModuleInitializer LogicInitializer => null;

        public virtual Priority Priority => Priority.Simple;
        
        public abstract string Name { get; }

        public IInjectionBinder InjectorBinder;
        
        public ICommandInjector CommandBinder;

        public void ExecuteAfterBindings(IInjectionBinder injector, ICommandInjector commandInjector)
        {
            InjectorBinder = injector;
            CommandBinder = commandInjector;
            ExecuteAfterBindings();
        }

        protected abstract void ExecuteAfterBindings();
    }
    
    public enum Priority
    {
        Highest,
        High,
        Simple,
        Low,
    }
}