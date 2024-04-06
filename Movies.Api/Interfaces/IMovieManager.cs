using Movies.Api.DTOs;

// Interface for the MovieManager class
public interface IMovieManager
{
    // This method returns a list of all movies - MovieDTO
    IList<MovieDTO> GetAllMovies();

    // This method adds a new movie to the database and returns it as a MovieDTO
    Task<MovieDTO> AddMovieAsync(MovieDTO movieDTO);
}