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

            // Agregar los parámetros
            foreach (var parameter in operation.parameters)
            {
                command.Parameters.Add(parameter);
            }

            // Abrimos conexión a la base
            connection.Open();

            // Ejecutamos el comando
            command.ExecuteNonQuery();
        }

        public Dictionary<string, object> ExecuteStoredProcedureWithUniqueResult(SqlOperation operation)
        {
            var result = new Dictionary<string, object>();
            SqlConnection connection = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand
            {
                Connection = connection,
                CommandText = operation.ProcedureName,
                CommandType = CommandType.StoredProcedure
            };

            // Agregar los parámetros
            foreach (var parameter in operation.parameters)
            {
                command.Parameters.Add(parameter);
            }

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            // Revisamos si tenemos filas
            if (reader.HasRows)
            {
                // Itera sobre todas las filas
                while (reader.Read())
                {
                    // Recorre los campos de cada fila
                    for (var fieldCount = 0; fieldCount < reader.FieldCount; fieldCount++)
                    {
                        result.Add(reader.GetName(fieldCount), reader.GetValue(fieldCount));
                    }
                }
            }

            return result;
        }

        public List<Dictionary<string, object>> ExecuteProcedureWithQuery(SqlOperation operation)
        {
            var result = new List<Dictionary<string, object>>();
            SqlConnection connection = new SqlConnection(_connectionString);

            SqlCommand command = new SqlCommand
            {
                Connection = connection,
                CommandText = operation.ProcedureName,
                CommandType = CommandType.StoredProcedure
            };

            // Agregar los parámetros
            foreach (var parameter in operation.parameters)
            {
                command.Parameters.Add(parameter);
            }

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            // Revisamos si tenemos filas
            if (reader.HasRows)
            {
                // Itera sobre todas las filas
                while (reader.Read())
                {
                    Dictionary<string, object> row = new Dictionary<string, object>();
                    // Recorre los campos de cada fila
                    for (var fieldCount = 0; fieldCount < reader.FieldCount; fieldCount++)
                    {
                        row.Add(reader.GetName(fieldCount), reader.GetValue(fieldCount));
                    }

                    result.Add(row);
                }
            }

            return result;
        }
    }
}
