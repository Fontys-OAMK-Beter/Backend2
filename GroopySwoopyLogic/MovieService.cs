using GroopySwoopyDTO;
using GroopySwoopyInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroopySwoopyLogic
{
    public class MovieService
    {
        private readonly IMovieDataservice _dataservice;

        public MovieService(IMovieDataservice movieDataservice)
        {
            this._dataservice = movieDataservice;
        }

        public void Post()
        {
            _dataservice.Post();
        }
        public void RemoveMovie()
        {
            _dataservice.RemoveMovie();
        }

        public void AddMovie()
        {
            _dataservice.AddMovie();
        }

        public MovieDTO GetMovie() 
        {
            return _dataservice.GetMovie();
        }
    }
}
