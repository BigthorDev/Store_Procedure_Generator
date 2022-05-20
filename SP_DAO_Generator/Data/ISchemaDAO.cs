using SP_DAO_Generator.Models;

namespace SP_DAO_Generator.Data
{
    public interface ISchemaDAO
    {
        IEnumerable<TableColumnModel> GetTableColumns(string tableName);
        IEnumerable<TableColumnModel> GetTables();
    }
}