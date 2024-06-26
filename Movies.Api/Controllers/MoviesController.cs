﻿using Microsoft.AspNetCore.Authorization;
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
        /// if the user is an admin
        /// </summary>
        /// <param name="movieDTO"></param>
        /// <returns> Task<ActionResult> </returns>
        [Authorize(Roles = UserRoles.Admin)]
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

        // Async method to update a movie by id in the database and return it as a MovieDTO, if the user is an admin
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("{_id}")]
        public async Task<ActionResult> UpdateMovie(uint _id, [FromBody] MovieDTO updateMovieDTO)
        {
            // Async update the movie in the database and get the updated movie as a MovieDTO
            MovieDTO? updatedMovie = await _movieManager.UpdateMovie(_id, updateMovieDTO);

            // If the movie is not found, return NotFound
            if(updatedMovie == null)
            {
                return NotFound();
            }

            return Ok(updatedMovie);
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

        /// <summary>
        /// Async method to delete a movie by id from the database and return it as a MovieDTO
        /// if the user is an admin
        /// </summary>
        /// <param name="_id"> movie id </param>
        /// <returns> deleted movie as MovieDTO </returns>
        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{_id}")]
        public async Task<ActionResult> DeleteMovie(uint _id)
        {
            MovieDTO? movieDTO = await _movieManager.DeleteMovie(_id);

            if(movieDTO == null)
            {
                return NotFound();
            }

            return Ok(movieDTO);
        }
    }
}
