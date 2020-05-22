namespace Checkers
{
    using AppSetup;
    using AppContext = AppContext.AppContext;
    
    internal class Program
    {
        public static void Main(string[] args)
        {
            var context = new AppContext();
            context.Start();
            var program = new App(context.InjectionBinder);
            context.InjectionBinder.Inject(program);
            program.Init();
            program.ProcessInput();
            program.Exit();
        }
    }
}