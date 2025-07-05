using System;
using System.Collections.Generic;

namespace Gehenna
{
    public class ManagerContextFactory
    {
        private GameConfig config;
        private ManagerContainer container;
        
        private readonly Dictionary<Type, IContextFactory> factories  = new Dictionary<Type, IContextFactory>();

        public ManagerContextFactory(GameConfig config, ManagerContainer container, IEnumerable<IContextFactory> contextFactories)
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
        
        public ManagerContext CreateContext(Type managerType)
        {
            if (factories.TryGetValue(managerType, out var factory))
                return factory.Create(config, container);

            throw new ArgumentException($"Unsupported manager type: {managerType}");
        }
    }
}