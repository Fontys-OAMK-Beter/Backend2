using GroopySwoopyAPI.Models;
using GroopySwoopyDAL;
using GroopySwoopyDTO;
using GroopySwoopyLogic;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace GroopySwoopyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieService movieService;

        public MovieController() 
        {

            movieService = new MovieService(new MovieDataservice());
        }

        [HttpGet("{id}/movies")]
        public List<Movie> GetAllMoviesbyUserid(int id)
        {
            List<Movie> movies = new List<Movie>();

            //if (!Authorize())
            //{
            //    HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
            //    return user;
            //}

            List<MovieDTO> moviesDTO = movieService.GetAllMoviesbyUserid(id);

            foreach (var movieDTO in moviesDTO)
            {
                movies.Add(new Movie());
                movies.LastOrDefault().Id = movieDTO.Id;
                movies.LastOrDefault().votes = movieDTO.Votes;
               
            }

            return movies;


        }

        [Route("Movie")]
        [HttpPost]
        public void AddMovie(string MovieId, int EventId)
        {
            movieService MovieService = new movieService(new MovieDataservice());
            movieService.Add
        }

    }
}
