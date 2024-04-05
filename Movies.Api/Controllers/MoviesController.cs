using Microsoft.AspNetCore.Mvc;

namespace Movies.Api.Controllers
{
    // This class is used to handle the requests for the movies

    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieManager _movieManager;

        public MoviesController(IMovieManager movieManager)
        {
            _movieManager = movieManager;
        }
    }
}
