using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroopySwoopyDTO
{
    public class GroupDTO
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<int> MemberIDs { get; set; }
            public int AdminID { get; set; }
    }
}
