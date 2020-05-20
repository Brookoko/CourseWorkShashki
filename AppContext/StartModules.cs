namespace AppContext
{
    using DependencyInjection;
    using DependencyInjection.Commands;
    
    public class StartModules : Command
    {
        [Inject]
        public ModuleInitializerHolder ModuleInitializerHolder { get; set; }

        [Inject]
        public IInjectionBinder Injector { get; set; }
        
        public override void Execute()
        {
            foreach (var initializer in ModuleInitializerHolder.Initializers)
            {
                Injector.Inject(initializer);
                initializer.Prepare();
            }
        }
    }
}