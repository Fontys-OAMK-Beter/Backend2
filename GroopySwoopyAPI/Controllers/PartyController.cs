using GroopySwoopyAPI.Models;
using GroopySwoopyDAL;
using GroopySwoopyDTO;
using GroopySwoopyLogic;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroopySwoopyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartyController : ControllerBase
    {
        //void PromoteUser(int UserId, int PartyId);
        //PartyDTO GetParty(int PartyId);


        // GET: api/<ValuesController>
        [HttpGet("{id}")]
        public PartyDTO GetPartyById(int PartyId)
        {
            PartyService partyService = new PartyService(new PartyDataservice());
            PartyDTO partyDTO = partyService.GetPartyById(PartyId);
            return partyDTO;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpPost]
        public void post([FromBody] Party _party)
        {
            PartyService partyService = new PartyService(new PartyDataservice());
            PartyDTO party = new PartyDTO();
            party.Title = _party.Title;
            party.PictureURL = _party.PictureURL;
            partyService.Post(party, _party.UserID);
        }



        //get all users by partyid
        [HttpGet("{id}/Users")]
        public List<User> GetUsersByPartyID(int id)
        {
            PartyService partyService = new PartyService(new PartyDataservice());
            List<User> Users = new List<User>();
            var UserDTO = partyService.GetUsersByPartyID(id);
            foreach(var dto in UserDTO)
            {
                Users.Add(new User());
                Users.Last().Id = dto.Id;
                Users.Last().Name = dto.Name;
                Users.Last().Email = dto.Email;
                Users.Last().Role = dto.Role;
                Users.Last().PictureUrl = dto.PictureUrl;
            }

            return Users;
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

        [Route("User")]
        [HttpPost]
        public void AddUser(string Email, int PartyId)
        {
            PartyService partyService = new PartyService(new PartyDataservice());
            partyService.AddUser(Email, PartyId);
        }
    }
}
