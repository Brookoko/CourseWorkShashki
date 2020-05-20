namespace DependencyInjection
{
    using Commands;
    
    public abstract class Context
    {
        private IInjectionBinder injectionBinder;
        private ICommandInjector commandInjector;
        
        public IInjectionBinder InjectionBinder
        {
            get => injectionBinder ?? new InjectionBinder();
            private set => injectionBinder = value;
        }
        
        public ICommandInjector CommandInjector
        {
            get => commandInjector ?? new CommandInjector();
            private set => commandInjector = value;
        }
        
        public void Start()
        {
            Prepare();
            ApplyBindings();
            Launch();
        }
        
        protected virtual void Prepare()
        {
            InjectionBinder = new InjectionBinder();
            InjectionBinder.Bind<IInjectionBinder>().ToInstance(InjectionBinder);
            CommandInjector = new CommandInjector {InjectionBinder = InjectionBinder};
            InjectionBinder.Bind<ICommandInjector>().ToInstance(CommandInjector);
        }
        
        protected virtual void ApplyBindings()
        {
        }
        
        protected abstract void Launch();
    }
}