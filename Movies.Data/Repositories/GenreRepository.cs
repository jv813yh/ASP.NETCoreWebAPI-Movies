using Movies.Data.DbContexts;
using Movies.Data.Interfaces;
using Movies.Data.Models;

namespace Movies.Data.Repositories
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(MoviesDbContext dbContext) : base(dbContext)
        {
        }
    }
}
