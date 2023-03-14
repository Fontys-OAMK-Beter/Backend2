using GroopySwoopyDTO;
using GroopySwoopyInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroopySwoopyLogic
{
    public class EventService
    {
        private readonly IEventDataservice _Dataservice;


        public EventService(IEventDataservice eventDataservice)
        {
            this._Dataservice = eventDataservice;
        }

        public List<EventDTO> GetAllEventsByGroupId(int group_id)
        {
            return _Dataservice.GetAllEventsByGroupId(group_id);
        }

        public EventDTO GetSpecificGroupEvent(int EventId, int GroupId)
        {
            return _Dataservice.GetSpecificGroupEvent(EventId, GroupId);
        }
        public void Post(EventDTO @event)
        {
            _Dataservice.Post(@event);
        }
        public void Delete(int EventId)
        {
            _Dataservice.Delete(EventId);
        }
    }
}
