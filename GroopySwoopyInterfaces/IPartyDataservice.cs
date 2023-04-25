using GroopySwoopyDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroopySwoopyInterfaces
{
    public interface IPartyDataservice
    {
        void Post(PartyDTO party, int UserId);
        void RemoveUser(int UserId, int PartyId);
        void AddUser(int UserId, int PartyId);
        void PromoteUser(int UserId, int PartyId);
        PartyDTO GetPartyById(int PartyId);
    }
}
