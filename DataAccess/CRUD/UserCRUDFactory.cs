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

        public override void Delete(string id)
        {
            var operation = _mapper.GetDeleteStatement(id);
            _dao.ExectureStoredProcedure(operation);
        }

        public override List<T> RetrieveAll<T>()
        {
            var listaUsuarios = new List<T>();
            var operation = _mapper.GetRetrieveAllStatement();
            var result = _dao.ExecuteProcedureWithQuery(operation);

            if (result.Count > 0)
            {
                var mappedResults = _mapper.BuildObjects(result);

                foreach (var usuario in mappedResults)
                {
                    var usuarioConvertido = (T)Convert.ChangeType(usuario, typeof(T));
                    listaUsuarios.Add(usuarioConvertido);
                }
            }

            return listaUsuarios;
        }

        public override BaseClass RetrieveById(string id)
        {
            var operation = _mapper.GetRetrieveStatement(id);
            var result = _dao.ExecuteStoredProcedureWithUniqueResult(operation);
            return _mapper.BuildObject(result);
        }

        public override void Update(BaseClass entityDTO)
        {
            var operation = _mapper.GetUpdateStatement(entityDTO);
            _dao.ExectureStoredProcedure(operation);
        }
    }
}
