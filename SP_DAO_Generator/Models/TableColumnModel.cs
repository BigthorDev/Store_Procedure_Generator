using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_DAO_Generator.Models
{
    public class TableColumnModel
    {
        public string? ColumnName { get; set; }
        public string? DataType { get; set; }
        public string? ConstraintName { get; set; }
        public string? TableName { get; set; }
        public int? ColumnSize { get; set; }
    }
}
