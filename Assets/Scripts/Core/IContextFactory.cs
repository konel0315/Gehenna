using System;

namespace Gehenna
{
    public interface IContextFactory
    {
        public Type ManagerType { get; }
        ManagerContext Create(GameConfig config, ManagerContainer container);
    }
}