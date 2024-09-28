using DataAccess.DAO;
using DTO;

namespace DataAccess.Mappers
{
    public interface ICrudStatements
    {
        public SqlOperation GetCreateStatement(BaseClass entityDTO);
        public SqlOperation GetRetrieveStatement(BaseClass entityDTO);
        public SqlOperation GetRetrieveAllStatement();
        public SqlOperation GetUpdateStatement(BaseClass entityDTO);
        public SqlOperation GetDeleteStatement(BaseClass entityDTO);
    }
}
