using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DataAccess.CRUD;

namespace BusinessLogic
{
    public class UserManager
    {
        public void CreateUser(User user)
        {
            UserCRUDFactory userFactory = new UserCRUDFactory();
            userFactory.Create(user);
        }
    }
}
