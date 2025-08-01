using Sirenix.OdinInspector;

namespace Gehenna
{
    public class AtlasEntry
    {
        [ValueDropdown("@AtlasKey.AllKeys")]
        public string Key;
        public AtlasBundle Bundle;
    }
}