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
    public class ModelGenerator
    {
        private readonly IDataAccess _dataAccess;
        public ModelGenerator(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }


        public async Task Generate(string tableName,string basePath, string fileNamespace)
        {
            string outputFile = $"{basePath}/Models/{tableName}Model.cs";
            TableColumnModel[] columns = new SchemaDAO(_dataAccess).GetTableColumns(tableName).ToArray();
            if (columns.Length == 0)
                return;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.Threading.Tasks;");
            sb.AppendLine("");
            sb.AppendLine("namespace " + fileNamespace + ";");

            sb.AppendLine("public class " + tableName + "Model");
            sb.AppendLine("{");

            foreach (TableColumnModel column in columns)
            {
                string line = $"public {GetDataType(column)} {column.ColumnName} ";
                line += "{get; set;}";
                sb.AppendLine(line);
            }

            sb.AppendLine("}"); 


            using (StreamWriter sw = new StreamWriter(outputFile))
            {
                sw.Write(sb.ToString());
            }
        }

        private string GetDataType(TableColumnModel c)
        {
            string ret = "";

            if (c.DataType.Equals("int") || c.DataType.Equals("smallint"))
                ret = "int";
            else if (c.DataType.Equals("nvarchar") || c.DataType.Equals("varchar") || c.DataType.Equals("text"))
                ret = "string";
            else if (c.DataType.Equals("datetime"))
                ret = "DateTime";
            else if (c.DataType.Equals("bit"))
                ret = "bool";
            else if (c.DataType.Equals("float"))
                ret = "double";
            else if (c.DataType.Equals("money"))
                ret = "double";
            else if (c.DataType.Equals("decimal"))
                ret = "double";
            else if (c.DataType.Equals("numeric"))
                ret = "double";
            else if (c.DataType.Equals("timestamp"))
                ret = "string";

            return ret;
        }
    }
}
