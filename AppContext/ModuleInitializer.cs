namespace AppContext
{
    using System.Collections.Generic;
    using System.Linq;
    
    public interface IModuleInitializer
    {
        void Prepare();
    }

    public class ModuleInitializerHolder
    {
        public List<IModuleInitializer> Initializers { get; }

        public ModuleInitializerHolder(List<IModuleInstaller> installers)
        {
            Initializers = installers
                .Select(installer => installer.LogicInitializer)
                .Where(initializer => initializer != null)
                .ToList();
        }
    }
}