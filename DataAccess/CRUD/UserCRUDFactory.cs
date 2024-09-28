using DataAccess.DAO;
using DataAccess.Mappers;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class UserCRUDFactory : CRUDFactory
    {
        private UserMapper _mapper;
        private SqlDao _dao;
        public UserCRUDFactory() : base()
        {
            // Tener referencias a las operaciones
            _mapper = new UserMapper();
            // Tiene la conexión a la base de datos
            _dao = SqlDao.GetInstance();
        }
        public override void Create(BaseClass entityDTO)
        {
            // Obtener la operación de la base de datos
            SqlOperation operation = _mapper.GetCreateStatement(entityDTO);
            // Ejecutar la operación
            _dao.ExectureStoredProcedure(operation);
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }

        public override BaseClass RetrieveById(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
