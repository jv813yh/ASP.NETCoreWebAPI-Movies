using Movies.Data.DbContexts;
using Movies.Data.Interfaces;
using Movies.Data.Models;

namespace Movies.Data.Repositories
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MoviesDbContext dbContext) : base(dbContext)
        {

        }

        // Method to get all movies from the database according to the filter
        public IList<Movie> GetMoviesByFilter(
            uint? directorId, 
            uint? actorId, 
            string? genre, 
            int? fromYear, 
            int? toYear, 
            int? limit)
        {
            IQueryable<Movie> query = _dbSet;

            if (directorId != 0)
            {
                query = query.Where(m => m.DirectorId == directorId);
            }

            if (actorId != 0)
            {
                query = query.Where(m => m.Actors.Any(a => a.PersonId == actorId));
            }

            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(m => m.Genres.Any(g => g.Name == genre));
            }

            if (fromYear != 0)
            {
                query = query.Where(m => m.Year >= fromYear);
            }

            if (toYear != 0)
            {
                query = query.Where(m => m.Year <= toYear);
            }

            if (limit != 0)
            {
                query = query.Take(limit.Value);
            }

            return query.ToList();
        }
    }
}
