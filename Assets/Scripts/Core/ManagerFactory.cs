using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Gehenna
{
    public static class ManagerFactory
    {
        private static readonly Dictionary<Type, Func<ISubManager>> factoryMap = new()
        {
            { typeof(ResourceManager), () => new ResourceManager() },
            { typeof(AudioManager),    () => new AudioManager() },
            { typeof(InputManager),    () => new InputManager() },
            { typeof(UIManager),       () => new UIManager() },
            { typeof(PoolingManager),  () => new PoolingManager() },
            { typeof(MapManager),      () => new MapManager() },
            { typeof(DialogueManager),      () => new DialogueManager() },
            { typeof(GameFlowManager), () => new GameFlowManager() },
        };
        
        public static T CreateManager<T>() where T : class, ISubManager
        {
            if (factoryMap.TryGetValue(typeof(T), out var factory))
                return factory() as T;

            throw new ArgumentException($"Unsupported manager type: {typeof(T)}");
        }
    }
}