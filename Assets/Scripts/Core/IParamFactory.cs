using System;

namespace Gehenna
{
    public interface IParamFactory
    {
        public Type ManagerType { get; }
        ManagerParam Create(GameConfig config, ManagerContainer container);
    }
}