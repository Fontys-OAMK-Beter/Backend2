﻿using GroopySwoopyAPI.Models;
using GroopySwoopyDAL;
using GroopySwoopyDTO;
using GroopySwoopyLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace GroopySwoopyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        // GET api/<EventController>/5
        [HttpGet("{group_id}")]
        public IEnumerable<Event> GetAllEventsByGroupId(int group_id)
        {
            EventService eventService = new EventService(new EventDataservice());
            List<EventDTO> dbEvents = eventService.GetAllEventsByGroupId(group_id);


            List<Event> events = new List<Event>();
            foreach (var item in dbEvents)
            {
                events.Add(new Event());
                events.LastOrDefault().Title = item.Title;
                events.LastOrDefault().Id = item.Id;
                events.LastOrDefault().Description = item.Description;
                events.LastOrDefault().PictureURL = item.PictureUrl;
                events.LastOrDefault().StartTime = item.StartTime;
            }

            return events.ToArray();
        }

        // GET api/<EventController>/5
        [HttpGet("{EventId},{GroupId}")]
        public Event GetSpecificGroupEvent(int EventId, int GroupId)
        {
            EventService eventService = new EventService(new EventDataservice());
            EventDTO dbUser = eventService.GetSpecificGroupEvent(EventId, GroupId);


            Event @event = new Event();
            @event.Id = dbUser.Id;
            @event.StartTime = dbUser.StartTime;
            @event.Description = dbUser.Description;
            @event.Title = dbUser.Title;
            @event.PictureURL = dbUser.PictureUrl;
            

            return @event;
        }

        // POST api/<EventController>
        [HttpPost]
        public void Post([FromBody] Event _event)
        {
            EventService eventService = new EventService(new EventDataservice());
            EventDTO @event = new EventDTO();
            //@event.StartTime = StartTime;
            @event.Description = _event.Description;
            @event.Title = _event.Title;
            @event.GroupId = _event.group_id;
            @event.PictureUrl = _event.PictureURL;
            eventService.Post(@event);
        }

        // PUT api/<EventController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EventController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            EventService eventService = new EventService(new EventDataservice());
            eventService.Delete(id);
        }
    }
}
