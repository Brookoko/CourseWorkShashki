namespace DependencyInjection.Commands
{
    using Signals;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICommandBinder
    {
        ICommandBinder To<T>() where T : Command;

        void InSequence();
    }

    public class CommandBinder : ICommandBinder
    {
        private readonly BaseSignal signal;
        private readonly List<Type> commands = new List<Type>();

        private readonly IInjectionBinder binder;
        
        private bool isSequence;
        private bool shouldStop;
        private Type currentCommand;
        
        public CommandBinder(IInjectionBinder binder, BaseSignal signal)
        {
            this.binder = binder;
            this.signal = signal;
            signal.AddListener(ExecuteCommands);
        }

        public ICommandBinder To<T>() where T : Command
        {
            return To(typeof(T));
        }

        public void InSequence()
        {
            isSequence = true;
        }

        private ICommandBinder To(Type commandType)
        {
            commands.Add(commandType);
            return this;
        }

        private async void ExecuteCommands(object[] parameters)
        {
            foreach (var command in commands)
            {
                currentCommand = command;
                var injectedTypes = InjectSignalParameters(parameters);
                var cmd = GetInstance(command);
                CleanUp(injectedTypes);
                await ExecuteCommand(cmd);
                if (shouldStop) break;
            }
            currentCommand = null;
        }

        private List<Type> InjectSignalParameters(object[] parameters)
        {
            var types = signal.GetTypes();
            var injectedTypes = new List<Type>();
            foreach (var type in types)
            {
                var isFound = false;
                foreach (var parameter in parameters)
                {
                    if (parameter == null)
                        throw new InjectionException("Attempt to bind null value into Command: " + currentCommand);
                    if (type.IsInstanceOfType(parameter))
                    {
                        binder.Bind(type).ToInstance(parameter);
                        injectedTypes.Add(type);
                        isFound = true;
                    }
                }
                if (!isFound)
                {
                    throw new InjectionException("Cannot find value to inject into Command: " + currentCommand + " for type: " + type);
                }
            }
            return injectedTypes;
        }

        private Command GetInstance(Type type)
        {
            binder.Bind<Command>().To(type);
            var command = binder.Get<Command>();
            binder.Unbind<Command>();
            return command;
        }
        
        private void CleanUp(List<Type> injectedTypes)
        {
            foreach (var type in injectedTypes)
            {
                binder.Unbind(type);
            }
        }

        private async Task ExecuteCommand(Command command)
        {
            command.Execute();
            if (isSequence && command.ReleaseTask != null) await command.ReleaseTask.Task;
            if (isSequence && command.IsFailed) shouldStop = true;
        }
    }
}