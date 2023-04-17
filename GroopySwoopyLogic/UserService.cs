using GroopySwoopyInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroopySwoopyDTO;
using System.Net.Http;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace GroopySwoopyLogic
{
    public class UserService
    {
        private readonly IUserDataservice _Dataservice;

        public List<string> Users { get; set; }


        public UserService(IUserDataservice userdataservice)
        {
            this._Dataservice = userdataservice;
        }

        public UserService(List<string> users)
        {
            this.Users = users;
        }

        public List<UserDTO> GetAllUsers()
        {
            return _Dataservice.GetAllUsers();
        }

        public UserDTO GetUserByID(int id)
        {
            return _Dataservice.GetUserByID(id);
        }
        public void Post(UserDTO user)
        {
            _Dataservice.Post(user);
        }

        public string LoginUser(string email, string password)
        {
            UserDTO user = new UserDTO();
            user.Email = email;
            user.Password = password;
            user.Id = _Dataservice.VerifyLoginCredentials(user);

            string Token = CreateJWT(user);

            if(_Dataservice.SetAuthToken((int)user.Id, Token))
                return Token;

            return null;
        }

        public Boolean AuthorizeUser(string Token)
        {
            return _Dataservice.AuthorizeUser(Token);
        }

        private string CreateJWT(UserDTO _user)
        {
            // Set the secret key for the JWT
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key_here"));

            // Create the token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    //new Claim(ClaimTypes.Name, "Mike"), //_user.Name),
                    new Claim(ClaimTypes.Email, _user.Email)
                }),
                Expires = DateTime.UtcNow.AddSeconds(30),
                SigningCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature)
            };

            // Create the JWT handler
            var jwtHandler = new JwtSecurityTokenHandler();

            // Generate the JWT token
            var token = jwtHandler.CreateToken(tokenDescriptor);

            // Get the JWT string
            var jwtString = jwtHandler.WriteToken(token);

            return jwtString;
        }
    }
}
