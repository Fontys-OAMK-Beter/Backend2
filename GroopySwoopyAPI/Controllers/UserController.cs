using Microsoft.AspNetCore.Mvc;
using GroopySwoopyLogic;
using GroopySwoopyDAL;
using GroopySwoopyDTO;
using GroopySwoopyInterfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroopySwoopyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            //UserService userService = new UserService(new UserDataservice());
            //List<UserDTO> dbUsers = userService.GetAllUsers();


            //List<User> users = new List<User>();
            //foreach (var item in dbUsers)
            //{
            //    users.Add(new User());
            //    users.LastOrDefault().Name = item.Name;
            //}

            //return users.ToArray();
            UserService userService = new UserService(new UserDataservice());
            UserDTO dbUser = userService.GetUserByID(id);


            User user = new User(); 
            user.Name = dbUser.Name;
            user.Id = dbUser.Id;
            user.Email = dbUser.Email;
            user.Password = dbUser.Password;

            return user;


        }

        // POST api/<UserController>
        [HttpPost]
        public void Post(string name, string email, string password)
        {
            UserService userService = new UserService(new UserDataservice());
            UserDTO user = new UserDTO();
            user.Name = name;
            user.Email = email;
            user.Password = password;
            userService.Post(user);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
