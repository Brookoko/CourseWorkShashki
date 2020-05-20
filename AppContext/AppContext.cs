namespace AppContext
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using DependencyInjection;
    
    public class AppContext : Context
    {
        private List<IModuleInstaller> modules = new List<IModuleInstaller>();
        
        protected override void Prepare()
        {
            base.Prepare();
            modules = FindModules();
        }

        private List<IModuleInstaller> FindModules()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(NeedToCheck)
                .SelectMany(ass => ass.GetExportedTypes())
                .Where(IsNeededType)
                .Select(type => (IModuleInstaller) Activator.CreateInstance(type))
                .OrderBy(module => module.Priority)
                .ToList();
        }

        private bool NeedToCheck(Assembly assembly)
        {
            var attribute = assembly.GetCustomAttributes(typeof(AssemblyModule), false);
            return attribute.Length > 0;
        }

        private bool IsNeededType(Type type)
        {
            return typeof(IModuleInstaller).IsAssignableFrom(type) &&
                   type != typeof(IModuleInstaller) &&
                   type != typeof(ModuleInstaller);
        }
        
        protected override void ApplyBindings()
        {
            base.ApplyBindings();
            InstallModules();
        }
        
        protected override void Launch()
        {
            InjectionBinder.Get<StartApp>().Dispatch();
        }

        private void InstallModules()
        {
            InjectionBinder.Bind<ModuleInitializerHolder>().ToInstance(new ModuleInitializerHolder(modules));
            
            foreach (var installer in modules)
            {
                InjectionBinder.Inject(installer);
                installer.ExecuteAfterBindings(InjectionBinder, CommandInjector);
            }
        }
    }
}