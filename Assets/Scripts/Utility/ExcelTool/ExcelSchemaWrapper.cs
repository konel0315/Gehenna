using Sirenix.OdinInspector;

namespace Gehenna
{
    public class ExcelSchemaWrapper
    {
        private ExcelSchema schema;
        
        public ExcelSchema Schema => schema;
        
        public ExcelSchemaWrapper(ExcelSchema schema)
        {
            this.schema = schema;
        }

        [TableColumnWidth(120)]
        [ShowInInspector]
        public string SchemaName
        {
            get => this.schema.schemaName;
            set => this.schema.schemaName = value;
        }
    }
}