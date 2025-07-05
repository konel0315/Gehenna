using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace Gehenna
{
    public class ExcelSchemaTable
    {
        [TableList(IsReadOnly = true, AlwaysExpanded = true), ShowInInspector]
        private readonly List<ExcelSchemaWrapper> schemas;
        
        public ExcelSchemaTable(IEnumerable<ExcelSchema> schemas)
        {
            this.schemas = schemas.Select(x => new ExcelSchemaWrapper(x)).ToList();
        }
    }
}