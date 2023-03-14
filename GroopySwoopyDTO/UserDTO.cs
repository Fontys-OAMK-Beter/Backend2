using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroopySwoopyDTO
{
    public class UserDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }        

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Role { get; set; }

        public string? ConfirmationLink { get; set; }

        public string? PictureUrl { get; set; }

        public bool? AccountConfirmed { get; set; } 


    }
}
