using SP_DAO_Generator.DbAccess;
using SP_DAO_Generator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_DAO_Generator.Data
{
    public class SchemaDAO : ISchemaDAO
    {
        private readonly IDataAccess _db;

        public SchemaDAO(IDataAccess db)
        {
            _db = db;
        }

        public IEnumerable<TableColumnModel> GetTables()
        {
            string SQL = "SELECT DISTINCT TABLE_NAME as TableName FROM [INFORMATION_SCHEMA].[TABLES] WHERE TABLE_TYPE='BASE TABLE' ORDER BY TABLE_NAME";
            var results = _db.LoadData<TableColumnModel, dynamic>(SQL, new { });
            return results;
        }

        public IEnumerable<TableColumnModel> GetTableColumns(string tableName)
        {
            string SQL = @"
                SELECT INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME as ColumnName, 
                INFORMATION_SCHEMA.COLUMNS.DATA_TYPE as DataType, 
                INFORMATION_SCHEMA.COLUMNS.CHARACTER_MAXIMUM_LENGTH as ColumnSize, 
                INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.CONSTRAINT_NAME as ConstraintName 	 
                from INFORMATION_SCHEMA.COLUMNS  
                LEFT OUTER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE  
	                ON INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME = INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.COLUMN_NAME  
	                AND INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.TABLE_NAME 
                WHERE INFORMATION_SCHEMA.COLUMNS.TABLE_NAME=@TableName 
                ";
            var results = _db.LoadData<TableColumnModel, dynamic>(SQL, new { TableName = tableName });
            return results;
        }

    }
}
