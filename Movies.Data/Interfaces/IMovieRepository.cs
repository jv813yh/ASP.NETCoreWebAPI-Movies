using Movies.Data.Models;

namespace Movies.Data.Interfaces
{
    public interface IMovieRepository : IBaseRepository<Movie>
    {
        IList<Movie> GetMoviesByFilter(
            uint? directorId,
            uint? actorId,
            string? genre,
            int? fromYear,
            int? toYear,
            int? limit);
    }
}
