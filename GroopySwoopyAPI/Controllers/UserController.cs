﻿using Microsoft.AspNetCore.Mvc;
using GroopySwoopyLogic;
using GroopySwoopyDAL;
using GroopySwoopyDTO;
using GroopySwoopyInterfaces;
using Microsoft.AspNetCore.Session;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroopySwoopyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;
        public UserController() {

            userService = new UserService(new UserDataservice());
        }

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

            User user = new User();

            if (!Authorize())
            {
                HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                return user;
            }

            UserDTO dbUser = userService.GetUserByID(id);

            user.Name = dbUser.Name;
            user.Email = dbUser.Email;
            user.Password = dbUser.Password;

            return user;


        }

        // POST api/<UserController>
        [Route("register")]
        [HttpPost]
        public void Register([FromBody] User _user)
        {
            UserDTO user = new UserDTO();
            user.Name = _user.Name;
            user.Email = _user.Email;
            user.Password = _user.Password;
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

        [Route("login")]
        [HttpPost]
        public void Login([FromBody] User _user)
        {
            if (Authorize())
                return;

            string Token = userService.LoginUser(_user.Email, _user.Password);

            if (Token != null)
                HttpContext.Response.Headers.Authorization = Token;
            else
                HttpContext.Response.StatusCode = new BadRequestResult().StatusCode;
        }

        private Boolean Authorize()
        {
            if (HttpContext.Request.Headers.Authorization.Count == 0)
                return false;

            if (userService.AuthorizeUser(HttpContext.Request.Headers.Authorization.First()))
                return true;

            return false;
        }

        [Route("logout")]
        [HttpPost]
        public void Logout()
        {
            HttpContext.Session.Clear();
        }
    }
}
