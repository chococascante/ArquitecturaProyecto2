using DataAccess.DAO;
using DTO;

namespace DataAccess.Mappers
{
    public interface ICrudStatements
    {
        public SqlOperation GetCreateStatement(BaseClass entityDTO);
        public SqlOperation GetRetrieveStatement(string id);
        public SqlOperation GetRetrieveAllStatement();
        public SqlOperation GetUpdateStatement(BaseClass entityDTO);
        public SqlOperation GetDeleteStatement(string id);
    }
}
