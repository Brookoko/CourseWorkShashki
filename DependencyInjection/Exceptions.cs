namespace DependencyInjection
{
    using System;

    public class BindingException : Exception
    {
        public BindingException()
        {
        }
        
        public BindingException(string message) : base(message)
        {
        }
    }
    
    public class InjectionException : Exception
    {
        public InjectionException()
        {
        }
        
        public InjectionException(string message) : base(message)
        {
        }
    }
}