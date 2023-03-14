using GroopySwoopyDTO;
using GroopySwoopyInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroopySwoopyLogic
{
    public class GroupService
    {
        private readonly IGroupDataservice _Dataservice;


        public GroupService(IGroupDataservice groupDataservice)
        {
            this._Dataservice = groupDataservice;
        }

        public void Post(GroupDTO group)
        {
            _Dataservice.Post(group);
        }
    }
}
