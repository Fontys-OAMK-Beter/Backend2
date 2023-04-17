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
using System.Security.Cryptography;

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

            if (user.Id < 0)
                return null;
            
            string Token = GenerateJWT(user);

            if(_Dataservice.SetAuthToken((int)user.Id, Token))
                return Token;

            return null;
        }

        public Boolean AuthorizeUser(string Token)
        {
            return _Dataservice.AuthorizeUser(Token);
        }

        private string GenerateJWT(UserDTO _user)
        {
            var secretKey = GenerateSecretKey();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, _user.Name),
                    new Claim(ClaimTypes.Email, _user.Email),
                    new Claim("kid", secretKey)
                }),
                Expires = DateTime.UtcNow.AddSeconds(30), //CHANGE IN PRODUCTION
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)), SecurityAlgorithms.HmacSha256Signature),
            };

            var jwtHandler = new JwtSecurityTokenHandler();

            var token = jwtHandler.CreateToken(tokenDescriptor);

            var jwtString = jwtHandler.WriteToken(token);

            return "Bearer " + jwtString;
        }

        private string GenerateSecretKey()
        {
            var key = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }

            var secretKey = Convert.ToBase64String(key);

            return secretKey;
        }

        private Boolean VerifyJWT(string _token)
        {
            // Get the JWT from the request headers or wherever it is stored
            string token = _token;

            // Define the validation parameters for the JWT
            var validationParameters = new TokenValidationParameters
            {
                //ValidateIssuer = true,
                //ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                //ValidIssuer = "your issuer here",
                //ValidAudience = "your audience here",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your secret key here"))
            };

            // Use the JwtSecurityTokenHandler to validate the JWT and extract its claims
            var handler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal;
            try
            {
                principal = handler.ValidateToken(token, validationParameters, out var validatedToken);
            }
            catch (SecurityTokenException)
            {
                // Invalid token
                return false;
            }

            // Extract the claims from the validated token
            var claims = principal.Claims;

            return true;
        }
    }
}
