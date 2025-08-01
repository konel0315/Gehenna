using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Gehenna
{
    public class CharacterTableSO : BaseTableSO
    {
        public List<CharacterTable> Tables;

        public IReadOnlyList<CharacterTable> GetTables() => Tables;
    }
}
