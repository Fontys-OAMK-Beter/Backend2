using GroopySwoopyDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroopySwoopyInterfaces
{
    public interface IEventDataservice
    {
        EventDTO GetSpecificGroupEvent(int EventId, int GroupId);
        List<EventDTO> GetAllEventsByGroupId(int id);
        void Post(EventDTO @event);
        void Delete(int id);
    }
}
