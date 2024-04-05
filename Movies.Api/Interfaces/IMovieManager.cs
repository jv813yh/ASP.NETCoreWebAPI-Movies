using Movies.Api.DTOs;

public interface IMovieManager
{
    IList<MovieDTO> GetAllMovies();

    Task<MovieDTO> AddMovieAsync(MovieDTO movieDTO);
}