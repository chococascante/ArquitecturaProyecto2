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

        public void UpdateUser(User user) {
            UserCRUDFactory userFactory = new UserCRUDFactory();
            userFactory.Update(user);
        }

        public void DeleteUser(string id)
        {
            UserCRUDFactory userFactory = new UserCRUDFactory();
            userFactory.Delete(id);
        }

        public List<User> RetrieveAllUsers()
        {
            UserCRUDFactory userFactory = new UserCRUDFactory();
            return userFactory.RetrieveAll<User>();
        }

        public User RetrieveUserById(string id)
        {
            UserCRUDFactory userFactory = new UserCRUDFactory();
            return (User)userFactory.RetrieveById(id);
        }
    }
}
