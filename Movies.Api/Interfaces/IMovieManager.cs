using Movies.Api.DTOs;

// Interface for the MovieManager class
public interface IMovieManager
{
    // Method gets movie by id from the database and returns it as a 
    ExtendedMovieDTO? GetMovieById(uint id);

    // This method gets all movies from the database and returns them as a list of MovieDTOs 
    IList<MovieDTO>? ExecuteMovieFilter(MovieFilterDTO? movieFilterDTO);

    // This method adds a new movie to the database and returns it as a MovieDTO
    Task<MovieDTO> AddMovieAsync(MovieDTO movieDTO);

    // This method updates a movie by id in the database and returns it as a MovieDTO
    Task<MovieDTO?> UpdateMovie(uint id, MovieDTO updateMovieDTO);

    // This method deletes a movie by id from the database and returns it as a MovieDTO
    Task<MovieDTO?> DeleteMovie(uint id);
}