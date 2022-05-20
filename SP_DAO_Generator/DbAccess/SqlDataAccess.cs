using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using SP_DAO_Generator.Utils;

namespace SP_DAO_Generator.DbAccess
{
    public class SqlDataAccess : IDataAccess
    {

        public IEnumerable<T> LoadData<T, U>(
            string sqlCommand,
            U parameters,
            string connectionID = "Default")
        {
            string connectionString = ConfigUtil.GetConnectionString(connectionID);
            using IDbConnection connection = new SqlConnection(connectionString);
            return connection.Query<T>(sqlCommand, parameters, commandType: CommandType.Text);
        }

        public int InsertData<U>(
            string sqlCommand,
            U parameters,
            string connectionID = "Default")
        {
            string connectionString = ConfigUtil.GetConnectionString(connectionID);
            using IDbConnection connection = new SqlConnection(connectionString);
            var response = Convert.ToInt32(connection.ExecuteScalar(sqlCommand, parameters, commandType: CommandType.Text));
            return response;

        }

        public void SaveData<T>(
            string sqlCommand,
            T parameters,
            string connectionID = "Default")
        {
            string connectionString = ConfigUtil.GetConnectionString(connectionID);
            using IDbConnection connection = new SqlConnection(connectionString);
            connection.Execute(sqlCommand, parameters, commandType: CommandType.Text);

        }
    }
}
