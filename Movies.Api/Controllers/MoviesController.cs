using Microsoft.AspNetCore.Mvc;
using Movies.Api.DTOs;

namespace Movies.Api.Controllers
{
    // This class is used to handle the requests for the movies

    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieManager _movieManager;

        public MoviesController(IMovieManager movieManager)
        {
            _movieManager = movieManager;
        }

        /// <summary>
        /// Async method that adds a new movie to the database and returns it
        /// </summary>
        /// <param name="movieDTO"></param>
        /// <returns> Task<ActionResult> </returns>
        [HttpPost]
        public async Task<ActionResult> AddNewMovieAsync([FromBody] MovieDTO movieDTO)
        {
            MovieDTO createdMovie = await _movieManager.AddMovieAsync(movieDTO);

            // Return the created movie as a response with the CreatedAtAction
            // so 201 status and the location of the movie and the movie itself
            return CreatedAtAction(nameof(GetMovieById), new { _id = createdMovie.Id }, createdMovie);
        }

        /// <summary>
        /// Method to get all movies from the database according to the filter and return them as a list of MovieDTOs
        /// </summary>
        /// <param name="movieFilterDTO"></param>
        /// <returns> IList<MovieDTO> </returns>
        [HttpGet]
        public ActionResult GetMovies([FromQuery] MovieFilterDTO movieFilterDTO)
        {
            IList<MovieDTO>? movies = _movieManager.ExecuteMovieFilter(movieFilterDTO);

            return Ok(movies);
        }


        /// <summary>
        /// Method to get a movie by id from the database and return it as a MovieDTO
        /// </summary>
        /// <param name="_id"></param>
        /// <returns> MovieDTO </returns>
        [HttpGet("{_id}")]
        public ActionResult GetMovieById(uint _id)
        {
            ExtendedMovieDTO? movie = _movieManager.GetMovieById(_id);

            // If the movie is not found, return NotFound
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }
    }
}
