using Sirenix.OdinInspector;

namespace Gehenna
{
    public abstract class BaseConfig : SerializedScriptableObject
    {
        public abstract void Initialize();
    }
}