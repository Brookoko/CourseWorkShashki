namespace Checkers.AppSetup
{
    using AppContext;
    using GameStatus;
    using Commands;
    using GameField;
    using Movements;
    
    public class AppModule : ModuleInstaller
    {
        public override IModuleInitializer LogicInitializer => new Initializer();
        
        public override string Name => "App";
        
        protected override void ExecuteAfterBindings()
        {
            InjectorBinder.Bind<IMovementProvider>().To<MovementProvider>().ToSingleton();
            InjectorBinder.Bind<ICommandQueue>().To<CommandQueue>().ToSingleton();
            InjectorBinder.Bind<IGameStatusProvider>().To<GameStatusProvider>().ToSingleton();
            InjectorBinder.Bind<IFieldProvider>().To<FieldProvider>().ToSingleton();
            
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