namespace DependencyInjection
{
    using System;
    using JetBrains.Annotations;
    
    [AttributeUsage(AttributeTargets.Property)]
    [MeansImplicitUse(ImplicitUseKindFlags.Assign)]
    public class Inject : Attribute
    {
    }
    
    [AttributeUsage(AttributeTargets.Method)]
    [MeansImplicitUse]
    public class PostConstruct : Attribute
    {
    }
}