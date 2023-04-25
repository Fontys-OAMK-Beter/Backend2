using GroopySwoopyDTO;
using GroopySwoopyInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroopySwoopyLogic
{
    public class PartyService
    {
        private readonly IPartyDataservice _Dataservice;


        public PartyService(IPartyDataservice partyDataservice)
        {
            this._Dataservice = partyDataservice;
        }

        public void Post(PartyDTO party, int UserId)
        {
            _Dataservice.Post(party, UserId);
        }

        public void RemoveUser(int UserId, int PartyId)
        {
            _Dataservice.RemoveUser(UserId, PartyId);
        }
        public void AddUser(int UserId, int PartyId)
        {
            _Dataservice.RemoveUser(UserId, PartyId);
        }

        public void PromoteUser(int UserId, int PartyId)
        {
            _Dataservice.RemoveUser(UserId, PartyId);
        }
        public PartyDTO GetPartyById(int PartyId)
        {
            return _Dataservice.GetPartyById(PartyId);
        }
    }
}
