using SP_DAO_Generator.Data;
using SP_DAO_Generator.DbAccess;
using SP_DAO_Generator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_DAO_Generator.Utils
{
    public class SpGenerator
    {
        private readonly IDataAccess _dataAccess;
        private readonly string _tableName;
        private readonly string _basePath;
        private StringBuilder sb;
        public SpGenerator(IDataAccess dataAccess, string tableName, string basePath)
        {
            _dataAccess = dataAccess;
            _tableName = tableName; 
            _basePath = basePath;   
        }

        public async Task Generate()
        {
            try
            {
                TableColumnModel[] columns = new SchemaDAO(_dataAccess).GetTableColumns(_tableName).ToArray();
                if (columns.Length == 0)
                    return;

                await GetAll_SP(columns);
                await GetByID_SP(columns);
                await Insert_SP(columns);
                await Update_SP(columns);
                await Delete_SP(columns);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task GetAll_SP(TableColumnModel[] columns)
        {
            string outputFile = $"{_basePath}/SP/{_tableName}_GetAll.sql";
            sb = new StringBuilder();
            sb.AppendLine($"CREATE PROCEDURE [dbo].[{_tableName}_GetAll]");
            sb.AppendLine("AS");
            sb.AppendLine("");
            sb.AppendLine("begin");

            string sql = "Select ";
            foreach (TableColumnModel c in columns)
            {
                sql += $"{c.ColumnName},";
            }
            sql = sql.Substring(0, sql.Length - 1);
            sb.AppendLine(sql);
            sb.AppendLine($" FROM dbo.{_tableName} ");

            sb.AppendLine("end");
            await GenerateDocument(sb, outputFile);
        }

        private async Task GetByID_SP(TableColumnModel[] columns)
        {
            string outputFile = $"{_basePath}/SP/{_tableName}_Get.sql";
            TableColumnModel primaryKey = GetPKColumn(columns);

            sb = new StringBuilder();
            sb.AppendLine($"CREATE PROCEDURE [dbo].[{_tableName}_Get]");

            string fieldSize = primaryKey.ColumnSize > 0 ? "(" + primaryKey.ColumnSize.ToString() + ")" : "";

            sb.AppendLine($"@{primaryKey.ColumnName} {primaryKey.DataType + fieldSize}");
            sb.AppendLine("AS");
            sb.AppendLine("");
            sb.AppendLine("begin");
            string sql = "Select ";
            foreach (TableColumnModel c in columns)
            {
                sql += $"{c.ColumnName},";
            }
            sql = sql.Substring(0, sql.Length - 1);
            sb.AppendLine(sql);
            sb.AppendLine($" FROM dbo.{_tableName} ");
            sb.AppendLine(" WHERE ");
            
            sb.AppendLine($"{primaryKey.ColumnName} = @{primaryKey.ColumnName};");

            sb.AppendLine("end");
            await GenerateDocument(sb, outputFile);
        }

        private async Task Insert_SP(TableColumnModel[] columns)
        {
            string outputFile = $"{_basePath}/SP/{_tableName}_Insert.sql";
            sb = new StringBuilder();
            sb.AppendLine($"CREATE PROCEDURE [dbo].[{_tableName}_Insert]");
            sb.AppendLine(GetParameters(columns, false));
            sb.AppendLine("AS");
            sb.AppendLine("");
            sb.AppendLine("begin");
            sb.AppendLine($"Insert Into {_tableName} (");

            string insertCols = "";
            string valueClause = "";
            foreach (TableColumnModel c in columns)
            {
                if (StringUtil.ToString(c.ConstraintName).StartsWith("PK_"))
                    continue;

                insertCols += $"{c.ColumnName},";
                valueClause += $"@{c.ColumnName},";
            }

            insertCols = insertCols.Substring(0, insertCols.Length - 1);
            valueClause = valueClause.Substring(0, valueClause.Length - 1);

            sb.AppendLine($"{insertCols}) VALUES ({valueClause})");

            sb.AppendLine("end");
            await GenerateDocument(sb, outputFile);
        }

        private async Task Update_SP(TableColumnModel[] columns)
        {
            string outputFile = $"{_basePath}/SP/{_tableName}_Update.sql";
            sb = new StringBuilder();            
            sb.AppendLine($"CREATE PROCEDURE [dbo].[{_tableName}_Update]");
            //Params
            sb.AppendLine(GetParameters(columns, true));
            sb.AppendLine("AS");
            sb.AppendLine("");
            sb.AppendLine("begin");

            sb.AppendLine($"Update {_tableName} SET");
            string updateClause = "";
            foreach (TableColumnModel c in columns)
            {
                if (StringUtil.ToString(c.ConstraintName).StartsWith("PK_"))
                    continue;

                updateClause += $"{c.ColumnName} = @{c.ColumnName},";
            }

            updateClause = updateClause.Substring(0, updateClause.Length - 1);
            sb.AppendLine(updateClause);
            TableColumnModel primaryKey = GetPKColumn(columns);

            sb.AppendLine($" WHERE {primaryKey.ColumnName}=@{primaryKey.ColumnName}");
            sb.AppendLine("end");
            await GenerateDocument(sb, outputFile);
        }

        private async Task Delete_SP(TableColumnModel[] columns)
        {
            string outputFile = $"{_basePath}/SP/{_tableName}_Delete.sql";
            TableColumnModel primaryKey = GetPKColumn(columns);

            sb = new StringBuilder();
            sb.AppendLine($"CREATE PROCEDURE [dbo].[{_tableName}_Delete]");
            string fieldSize = primaryKey.ColumnSize > 0 ? "(" + primaryKey.ColumnSize.ToString() + ")" : "";
            sb.AppendLine($"@{primaryKey.ColumnName} {primaryKey.DataType + fieldSize}");
            sb.AppendLine("AS");
            sb.AppendLine("");
            sb.AppendLine("begin");

            sb.AppendLine($"DELETE FROM {_tableName}");
            sb.AppendLine($" WHERE {primaryKey.ColumnName}=@{primaryKey.ColumnName}");
            sb.AppendLine("end");

            await GenerateDocument(sb,outputFile);
        }

        private async Task GenerateDocument(StringBuilder sb, string outputFile)
        {
            using (StreamWriter sw = new StreamWriter(outputFile))
            {
                sw.Write(sb.ToString());
            }
        }

        private TableColumnModel GetPKColumn(TableColumnModel[] columns)
        {
            TableColumnModel col = new TableColumnModel();
            foreach (TableColumnModel c in columns)
            {
                //skip the PK in insert
                if (StringUtil.ToString(c.ConstraintName).StartsWith("PK_"))
                {
                    col.ColumnName = c.ColumnName;
                    col.DataType = c.DataType;
                    col.ColumnSize = c.ColumnSize;
                    break;
                }
            }
            return col;
        }

        private string GetParameters(TableColumnModel[] columns,bool AddPrimaryKey)
        {
            string parameters = "";
            foreach (TableColumnModel c in columns)
            {

                if (!AddPrimaryKey)
                {
                    if (StringUtil.ToString(c.ConstraintName).StartsWith("PK_")) continue;
                }
                

                if (c.ColumnSize > 0)
                {
                    parameters += $"@{c.ColumnName} {c.DataType}({c.ColumnSize.ToString()}),";
                }
                else
                    parameters += $"@{c.ColumnName} {c.DataType},";
            }

            parameters = parameters.Substring(0, parameters.Length - 1);
            return parameters;
        }


    }
}
