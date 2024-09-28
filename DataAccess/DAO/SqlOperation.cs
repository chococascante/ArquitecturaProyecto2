using System.Data.SqlClient;

namespace DataAccess.DAO
{
    public class SqlOperation
    {
        public string ProcedureName { get; set; }
        public List<SqlParameter> parameters;

        public SqlOperation()
        {
            parameters = new List<SqlParameter>();
        }

        public void AddVarcharParameter(string parameterName, string parameterValue)
        {
            parameters.Add(new SqlParameter("@" + parameterName, parameterValue));
        }

        public void AddIntegerParameter(string parameterName, int parameterValue)
        {

        }
    }
}
