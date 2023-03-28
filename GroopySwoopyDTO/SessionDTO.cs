using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroopySwoopyDTO
{
    public class SessionDTO
    {
        public Guid SessionID { get; set; }
        public DateTime SessionTime { get; set; }

        public SessionDTO(Guid _sessionID, DateTime _sessionTime)
        {
            SessionID = _sessionID;
            SessionTime = _sessionTime;
        }
    }
}
