using System;
using System.Collections.Generic;

namespace Gehenna
{
    public class ManagerContainer
    {
        private readonly Dictionary<Type, ISubManager> managerMap = new Dictionary<Type, ISubManager>();
    
        public void Register<T>(Func<T> factory) where T : class, ISubManager
        {
            if (factory == null)
                throw new ArgumentNullException();

            Type type = typeof(T);
            if (managerMap.ContainsKey(type))
                throw new InvalidOperationException($"{type} is already registered.");

            managerMap[type] = factory();
        }

        public T Resolve<T>() where T : class, ISubManager
        {
            Type type = typeof(T);
            if (!managerMap.TryGetValue(type, out var manager))
                throw new InvalidOperationException($"{type} is not registered.");
            return manager as T;
        }
        
        public bool Contains<T>() where T : class, ISubManager
        {
            return managerMap.ContainsKey(typeof(T));
        }

        public IEnumerable<ISubManager> GetAll()
        {
            return managerMap.Values;
        }
    }
}

