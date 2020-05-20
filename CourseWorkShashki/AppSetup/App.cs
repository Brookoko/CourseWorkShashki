namespace AppSetup
{
    using ConsoleApp;
    using DependencyInjection;

    public class App : ConsoleProgram
    {
        public App(IInjectionBinder binder)
        {
            var start = (IOptions) binder.Inject(new StartOptions());
            start.AddOption("--exit", _ => StopProcessingInput());
            Options = start;
        }
    }
}