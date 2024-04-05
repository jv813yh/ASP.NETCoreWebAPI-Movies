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
        public ActionResult GetGenres()
        {
            IList<string>? genres = _genreManager.GetAllGenres();

            if (genres == null)
            {
                return NotFound();
            }

            return Ok(_genreManager.GetAllGenres());
        }
    }
}
