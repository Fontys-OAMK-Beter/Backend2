using GroopySwoopyDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroopySwoopyInterfaces
{
    public interface IGroupDataservice
    {
        void Post(GroupDTO group);
    }
}
