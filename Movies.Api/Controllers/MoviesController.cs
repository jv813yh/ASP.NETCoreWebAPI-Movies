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
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddNewMovie([FromBody] MovieDTO movieDTO)
        {
            MovieDTO createdMovie = await _movieManager.AddMovieAsync(movieDTO);

            return StatusCode(201, createdMovie);

           // return CreatedAtAction(nameof(GetMovieById), new { id = createdMovie.Id }, createdMovie);
        }
    }
}
