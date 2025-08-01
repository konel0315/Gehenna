using System;
using System.Collections.Generic;

namespace Gehenna
{
    public class GameFlowContainer
    {
        private readonly Dictionary<Type, BaseGameState> gameStateMap = new Dictionary<Type, BaseGameState>();
        private readonly Dictionary<Type, Func<BaseGameState>> factoryMap = new Dictionary<Type, Func<BaseGameState>>();
        
        public void Register<T>(Func<T> factory) where T : BaseGameState
        {
            factoryMap[typeof(T)] = () => factory();
        }

        public T Resolve<T>() where T : BaseGameState
        {
            Type type = typeof(T);
            if (!gameStateMap.TryGetValue(type, out var state))
            {
                if (!factoryMap.TryGetValue(type, out var factory))
                    throw new InvalidOperationException($"No factory registered for {type}");

                state = factory();
                gameStateMap[type] = state;
            }
            return (T)state;
        }
        
        public bool Contains<T>() where T : class, ISubManager
        {
            return gameStateMap.ContainsKey(typeof(T));
        }

        public IEnumerable<BaseGameState> GetEnumerator()
        {
            return gameStateMap.Values;
        }
    }
}