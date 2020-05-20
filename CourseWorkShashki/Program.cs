namespace lab6
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
            program.Init();
            program.ProcessInput();
            program.Exit();
        }
    }
}