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

        public void DeleteUserByID(int id)
        {
            _Dataservice.DeleteUserByID(id);
        }

        public void UpdateUser(UserDTO user)
        {
            _Dataservice.UpdateUser(user);
        }

        public void Post(UserDTO user)
        {
            _Dataservice.Post(user);
        }

        public List<PartyDTO> GetPartiesByUserId(int id)
        {
            return _Dataservice.GetPartiesByUserId(id);
        }

        public string LoginUser(string email, string password)
        {
            UserDTO user = new UserDTO();
            user.Email = email;
            user.Password = password;
            user = _Dataservice.VerifyLoginCredentials(user);

            if (user.Id < 0)
                return null;
            
            string Token = GenerateJWT(user);

            if(_Dataservice.SetAuthToken((int)user.Id, Token))
                return Token;

            return null;
        }

        public Boolean AuthorizeUser(string Token)
        {
            if(VerifyJWT(Token))
                return _Dataservice.AuthorizeUser(Token);

            return false;
        }

        private string GenerateJWT(UserDTO _user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("J5h@3qLdP#vRzK9X!cF#2h6%1b$yE@7a"));

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, _user.Name),
                new Claim(ClaimTypes.Email, _user.Email),
                new Claim(ClaimTypes.UserData, _user.Id.ToString()),
            };

            // create a signing key using the secret key and the HMACSHA256 algorithm
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);

            // create the JWT token
            var token = new JwtSecurityToken(
                issuer: "GroopySwoopy",
                audience: "GroopyGang",
                claims: claims,
                expires: DateTime.Now.AddSeconds(30),
                signingCredentials: signingCredentials);

            // create a JWT token handler and write the token to a string
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);
            return "Bearer " + tokenString;
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
            string token = _token.Remove(0,7);

            // Define the validation parameters for the JWT
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                //ValidIssuer = "your issuer here",
                //ValidAudience = "your audience here",
                RequireExpirationTime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("J5h@3qLdP#vRzK9X!cF#2h6%1b$yE@7a"))
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

            JwtSecurityToken jwtSecurityToken;
            try
            {
                jwtSecurityToken = new JwtSecurityToken(token);
            }
            catch (Exception)
            {
                return false;
            }

            return jwtSecurityToken.ValidTo > DateTime.UtcNow;
        }
    }
}
