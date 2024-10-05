using DataAccess.DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    public class UserMapper : ICrudStatements, IObjectMapper
    {
        public BaseClass BuildObject(Dictionary<string, object> objectRow)
        {
            return new User()
            {
                Id = objectRow["id"].ToString(),
                Name = objectRow["nombre"].ToString(),
                LastName = objectRow["apellido"].ToString(),
                Password = objectRow["password"].ToString(),
                ProfilePicture = objectRow["imagenPerfil"].ToString(),
                Email = objectRow["correoElectronico"].ToString()
            };
        }

        public List<BaseClass> BuildObjects(List<Dictionary<string, object>> objectRows)
        {
            List<BaseClass> usuarios = new List<BaseClass>();

            foreach(var row in objectRows)
            {
                var usuario = (User)BuildObject(row);
                usuarios.Add(usuario);
            }

            return usuarios;
        }

        public SqlOperation GetCreateStatement(BaseClass entityDTO)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "dbo.sp_insertarUsuario";

            User user = (User) entityDTO;

            operation.AddVarcharParameter("nombre", user.Name);
            operation.AddVarcharParameter("apellido", user.LastName);
            operation.AddVarcharParameter("correoElectronico", user.Email);
            operation.AddVarcharParameter("password", user.Password);
            operation.AddVarcharParameter("imagenPerfil", user.ProfilePicture);

            return operation;
        }

        public SqlOperation GetDeleteStatement(string id)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "dbo.sp_eliminarUsuario";

            operation.AddVarcharParameter("id", id);

            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            SqlOperation operation = new SqlOperation
            {
                ProcedureName = "dbo.sp_obtenerUsuarios"
            };

            return operation;
        }

        public SqlOperation GetRetrieveStatement(string id)
        {
            SqlOperation operation= new SqlOperation { ProcedureName = "dbo.sp_obtenerUsuarioPorId" };
            operation.AddVarcharParameter("id", id);

            return operation;

        }

        public SqlOperation GetUpdateStatement(BaseClass entityDTO)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "dbo.sp_actualizarUsuario";

            User user = (User)entityDTO;

            operation.AddVarcharParameter("id", user.Id);
            operation.AddVarcharParameter("nombre", user.Name);
            operation.AddVarcharParameter("apellido", user.LastName);
            operation.AddVarcharParameter("correoElectronico", user.Email);
            operation.AddVarcharParameter("password", user.Password);
            operation.AddVarcharParameter("imagenPerfil", user.ProfilePicture);

            return operation;
        }
    }
}
