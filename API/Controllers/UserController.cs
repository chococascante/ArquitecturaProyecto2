using Microsoft.AspNetCore.Mvc;
using DTO;
using BusinessLogic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            // return users
            var userManager = new UserManager();
            return userManager.RetrieveAllUsers();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(string id)
        {
            var userManager = new UserManager();
            return userManager.RetrieveUserById(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post(User value)
        {
            UserManager userManager = new UserManager();
            userManager.CreateUser(value);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(User value)
        {
            var userManager = new UserManager();
            userManager.UpdateUser(value);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(string value)
        {
            var userManager = new UserManager();
            userManager.DeleteUser(value);
        }
    }
}
