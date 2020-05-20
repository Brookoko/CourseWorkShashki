namespace DependencyInjection
{
    using System;
    using System.Reflection;
    
    public interface IInjectionBinder
    {
        object Inject(object obj);

        IBinder Bind<T>();

        IBinder Bind(Type type);

        void Unbind<T>();

        void Unbind(Type type);
        
        T Get<T>() where T : class;
        
        object Get(Type type);
    }
    
    public class InjectionBinder : IInjectionBinder
    {
        private readonly TypeMapping mapping = new TypeMapping();
        
        public object Inject(object obj)
        {
            var type = obj.GetType();
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var property in properties)
            {
                var attribute = (Inject) Attribute.GetCustomAttribute(property, typeof(Inject), false);
                if (attribute == null) continue;
                if (!property.CanWrite) throw new InjectionException($"Failed to inject into readonly property: {type}");
                property.SetValue(obj, Get(property.PropertyType));
            }
            return obj;
        }

        public IBinder Bind<T>()
        {
            return Bind(typeof(T));
        }

        public IBinder Bind(Type type)
        {
            return new Binder(type, mapping);
        }

        public void Unbind<T>()
        {
            Unbind(typeof(T));
        }

        public void Unbind(Type type)
        {
            mapping.RemoveBindingFor(type);
        }

        public T Get<T>() where T : class
        {
            return (T) Get(typeof(T));
        }

        public object Get(Type type)
        {
            var mapped = mapping.GetMapping(type);
            return PrepareAndInjectComponent(mapped, mapped.Instance ?? CreateInstance(mapped));
        }

        private object CreateInstance(Mapping mapping)
        {
            var type = mapping.Type;
            var constructor = type.GetConstructor(Type.EmptyTypes);
            var instance = constructor?.Invoke(new object[] { });
            if (instance == null) throw new InjectionException($"Failed to create instance for: {type}");
            return instance;
        }

        private object PrepareAndInjectComponent(Mapping mapping, object component)
        {
            component = Inject(component);
            if (mapping.ShouldPrepare)
            {
                PrepareComponent(component, component.GetType());
                if (mapping.Instance != null) mapping.ShouldPrepare = false;
            }
            return component;
        }

        private void PrepareComponent(object component, Type type)
        {
            var methods = type.GetMethods();
            foreach (var method in methods)
            {
                var attribute = method.GetCustomAttribute(typeof(PostConstruct));
                if (attribute != null)
                {
                    method.Invoke(component, new object[] { });
                }
            }
        }
    }
}