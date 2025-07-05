using System;
using System.Collections.Generic;

namespace Gehenna
{
    public class StateMachine<T>
    {
        private readonly T owner;
        private IState<T> currentState;
        private readonly Dictionary<Type, IState<T>> stateDict = new Dictionary<Type, IState<T>>();
        
        public StateMachine(T owner)
        {
            this.owner = owner;
        }
        
        public void CleanUp()
        {
            currentState?.Exit();
            currentState = null;
            stateDict.Clear();
        }
    
        public void AddState(IState<T> state)
        {
            Type type = state.GetType();
            if(stateDict.ContainsKey(type))
                throw new InvalidOperationException($"State {type} is already added.");
    
            stateDict[type] = state;
            state.Initialize(owner, this);
        }
        
        public void ChangeState<TState>() where TState : IState<T>
        {
            Type type = typeof(TState);
            
            if (!stateDict.TryGetValue(type, out var state))
                throw new InvalidOperationException($"State {type} not registered.");
            
            if (currentState == state)
                return;
    
            currentState?.Exit();
            currentState = state;
            currentState.Enter();
        }
    
        public void ManualUpdate()
        {
            currentState?.Update();
        }
    
        public void ManualLateUpdate()
        {
            currentState?.LateUpdate();
        }
    
        public void ManualFixedUpdate()
        {
            currentState?.FixedUpdate();
        }
    }
}