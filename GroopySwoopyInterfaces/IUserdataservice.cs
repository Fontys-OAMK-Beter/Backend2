﻿using System;
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
        List<PartyDTO> GetPartiesByUserId(int id);
        void Post(UserDTO user);
        UserDTO VerifyLoginCredentials(UserDTO user);
        Boolean AuthorizeUser(string Token);
        Boolean SetAuthToken(int UserID, string Token);
        void DeleteUserByID(int id);
        void UpdateUser(UserDTO user);
    }
}
