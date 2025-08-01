using System;
using System.Collections.Generic;

namespace Gehenna
{
    public class ManagerParamFactory
    {
        private GameConfig config;
        private ManagerContainer container;
        
        private readonly Dictionary<Type, IParamFactory> factories  = new Dictionary<Type, IParamFactory>();

        public ManagerParamFactory(GameConfig config, ManagerContainer container, IEnumerable<IParamFactory> contextFactories)
        {
            this.config = config;
            this.container = container;
            
            foreach (var factory in contextFactories)
            {
                if (factories.TryAdd(factory.ManagerType, factory))
                    continue;
                
                GehennaLogger.Log(this, LogType.Warning, $"Duplicate context factory for type: {factory.ManagerType}");
            }
        }
        
        public ManagerParam CreateContext(Type managerType)
        {
            if (factories.TryGetValue(managerType, out var factory))
                return factory.Create(config, container);

            throw new ArgumentException($"Unsupported manager type: {managerType}");
        }
    }
}