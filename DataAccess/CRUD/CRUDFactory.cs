using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DataAccess.CRUD
{
    public abstract class CRUDFactory
    {
        // DAO que no existe todavía
        public abstract void Create();
        public abstract void Update();
        public abstract void Delete();
        public abstract List<T> RetrieveAll<T>();
        public abstract BaseClass RetrieveById(int id);
    }
}
