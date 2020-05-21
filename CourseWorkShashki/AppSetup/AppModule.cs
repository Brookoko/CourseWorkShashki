namespace AppSetup
{
    using AppContext;
    using Commands;
    using Movements;
    
    public class AppModule : ModuleInstaller
    {
        public override IModuleInitializer LogicInitializer => new Initializer();
        
        public override string Name => "App";
        
        protected override void ExecuteAfterBindings()
        {
            InjectorBinder.Bind<IMovementProvider>().To<MovementProvider>().ToSingleton();
            InjectorBinder.Bind<ICommandQueue>().To<CommandQueue>().ToSingleton();
            
            CommandBinder.Bind<StartApp>()
                .To<StartModules>()
                .InSequence();
        }
        
        private class Initializer : IModuleInitializer
        {
            public void Prepare()
            {
            }
        }
    }
}