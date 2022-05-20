
namespace SP_DAO_Generator.DbAccess
{
    public interface IDataAccess
    {
        int InsertData<U>(string sqlCommand, U parameters, string connectionID = "Default");
        IEnumerable<T> LoadData<T, U>(string sqlCommand, U parameters, string connectionID = "Default");
        void SaveData<T>(string sqlCommand, T parameters, string connectionID = "Default");
    }
}