using Microsoft.AspNetCore.Mvc;
using Movies.Api.Interfaces;

namespace Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreManager _genreManager;

        public GenresController(IGenreManager genreManager)
        {
            _genreManager = genreManager;
        }

        /// <summary>
        ///  Get all genres
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetGenres()
        {
            return Ok(_genreManager.GetAllGenres());
        }
    }
}
