using GroopySwoopyAPI.Models;
using GroopySwoopyDAL;
using GroopySwoopyDTO;
using GroopySwoopyLogic;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroopySwoopyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartyController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpPost]
        public void post(string title, string pictureUrl, int UserId)
        {
            PartyService partyService = new PartyService(new PartyDataservice());
            PartyDTO party = new PartyDTO();
            party.Title = title;
            party.PictureURL = pictureUrl;
            partyService.Post(party, UserId);
        }



        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpDelete("")]
        public void RemoveUser(int UserId, int PartyId, [FromBody] Party partyModel) 
        {
            PartyService partyService = new PartyService(new PartyDataservice());
            PartyDTO party = new PartyDTO();
            partyService.RemoveUser(UserId, PartyId);
        }
    }
}
