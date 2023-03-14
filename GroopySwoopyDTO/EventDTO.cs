using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroopySwoopyDTO
{
    public class EventDTO
    {
        public int Id { get; set; }

        public int GroupId { get; set; }

        public DateTime StartTime { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public string PictureUrl { get; set; }
    }
}
