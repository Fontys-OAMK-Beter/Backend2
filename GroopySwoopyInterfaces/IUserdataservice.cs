using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroopySwoopyDTO;

namespace GroopySwoopyInterfaces
{
    public interface IUserDataservice
    {
        List<UserDTO> GetAllUsers();
        UserDTO GetUserByID(int id);
        void Post(UserDTO user);
    }
}
