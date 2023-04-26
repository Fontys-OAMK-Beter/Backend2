using GroopySwoopyDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroopySwoopyInterfaces
{
    public interface IMovieDataservice
    {
        void Post();

        void RemoveMovie();

        void AddMovie();
                
        MovieDTO GetMovie();
    }
}
