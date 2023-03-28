using GroopySwoopyInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroopySwoopyDTO;

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

        public Guid? LoginUser(string email, string password)
        {
            UserDTO user = new UserDTO();
            user.Email = email;
            user.Password = password;
            return _Dataservice.LoginUser(user);
        }

        public Boolean AuthorizeUser(Guid SessionID)
        {
            return _Dataservice.AuthorizeUser(SessionID);
        }
    }
}
