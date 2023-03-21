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
    }
}
