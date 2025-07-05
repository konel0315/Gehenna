using System;
using System.Collections.Generic;

namespace Gehenna
{
    public interface IPoolableCatalog
    {
        IEnumerable<IPoolableEntry> GetPoolableEntries();
    }
}