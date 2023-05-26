using Microsoft.AspNetCore.Mvc;
using GroopySwoopyLogic;
using GroopySwoopyDAL;
using GroopySwoopyDTO;
using GroopySwoopyInterfaces;
using Microsoft.AspNetCore.Session;
using GroopySwoopyAPI.Models;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

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
            User user = new User();
            UserDTO dbUser = userService.GetUserByID(id);

            user.Name = dbUser.Name;
            user.Email = dbUser.Email;
            user.Password = dbUser.Password;
            user.PictureUrl = dbUser.PictureUrl;
            user.Role = dbUser.Role;

            return user;


        }

        [HttpGet("{id}/parties")]
        public List<Party> GetPartiesByUserId(int id)
        {
            List<Party> parties = new List<Party>();

            List<PartyDTO> partiesDTO = userService.GetPartiesByUserId(id);

            foreach (var partyDTO in partiesDTO)
            {
                parties.Add(new Party());
                parties.LastOrDefault().Id = partyDTO.Id;
                parties.LastOrDefault().PictureURL = partyDTO.PictureURL;
                parties.LastOrDefault().Title = partyDTO.Title;
            }

            return parties;


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
        [HttpPut("update")]
        public void Put([FromBody] UserDTO _user)
        {
            userService.UpdateUser(_user);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            userService.DeleteUserByID(id);
        }

        [Route("login")]
        [HttpPost]
        public void Login([FromBody] User _user)
        {
            if (Authorize())
                return; //Already logged in

            string Token = userService.LoginUser(_user.Email, _user.Password);

            if (Token != null)
                Response.Headers.Add("Authorization", Token);
            else
                Response.StatusCode = new UnauthorizedResult().StatusCode;
        }

        private Boolean Authorize()
        {
            //No valid token
            if (Request.Headers.Authorization.Count == 0)
            {
                return false;
            }

            //Valid token
            if (userService.AuthorizeUser(Request.Headers.Authorization.First()))
                return true;

            return false;
        }

        [Route("logout")]
        [HttpPost]
        public void Logout()
        {
            Response.Headers.Remove("Authorization");
        }

        // get all users by partyid
        //[HttpGet("{partyid}")]
        //public UserDTO GetAllUsersbyPartyId(int PartyId)
        //{
        //    PartyService partyService = new PartyService(new PartyDataservice());
        //    PartyDTO partyDTO = partyService.GetPartyById(PartyId);
        //    return partyDTO;
        //}
    }
}
