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
    public class DaoGenerator
    {
        private readonly IDataAccess _dataAccess;
        private readonly string _tableName;
        private readonly string _basePath;
        public DaoGenerator(IDataAccess dataAccess, string tableName, string basePath)
        {
            _dataAccess = dataAccess;
            _tableName = tableName;
            _basePath = basePath;
        }

        ///TODO:
        public async Task Generate(string tableName, string basePath, string fileNamespace)
        {
            try
            {
                TableColumnModel[] columns = new SchemaDAO(_dataAccess).GetTableColumns(_tableName).ToArray();
                if (columns.Length == 0)
                    return;

                

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
