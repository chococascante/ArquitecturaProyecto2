using System.Data.SqlClient;
using System.Data;

namespace DataAccess.DAO
{
    // Singleton
    public class SqlDao
    {
        private static SqlDao _instance;
        private string _connectionString = "Server=localhost;Database=Proyecto2;Trusted_Connection=True";

        private SqlDao()
        {

        }

        public static SqlDao GetInstance()
        {
            if(_instance == null)
            {
                _instance = new SqlDao();
            }

            return _instance;
        }

        public void ExectureStoredProcedure(SqlOperation operation)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = operation.ProcedureName;
            command.CommandType = CommandType.StoredProcedure;
        }
    }
}
