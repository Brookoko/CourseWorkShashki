namespace AppSetup
{
    using AppContext;
    
    public class AppModule : ModuleInstaller
    {
        public override IModuleInitializer LogicInitializer => new Initializer();
        
        public override string Name => "App";
        
        protected override void ExecuteAfterBindings()
        {
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