namespace DependencyInjection.Commands
{
    using Signals;
    using System;
    
    public interface ICommandInjector
    {
        ICommandBinder Bind<T>() where T : BaseSignal;
    }
    
    public class CommandInjector : ICommandInjector
    {
        public IInjectionBinder InjectionBinder { get; set; }
        
        public ICommandBinder Bind<T>() where T : BaseSignal
        {
            return Bind(typeof(T));
        }
        
        private ICommandBinder Bind(Type type)
        {
            var constructor = type.GetConstructor(Type.EmptyTypes);
            var signal = (BaseSignal) constructor?.Invoke(new object[] { });
            InjectionBinder.Bind(type).ToInstance(signal);
            return new CommandBinder(InjectionBinder, signal);
        }
    }
}