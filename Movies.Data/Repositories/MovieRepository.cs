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
    }
}
