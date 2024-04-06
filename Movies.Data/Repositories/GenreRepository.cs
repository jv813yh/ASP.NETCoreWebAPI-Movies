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

        // Method to get all genres from the database according the genre name, 
        // It is used for mapping between the MovieDTO and the Movie
        public IList<Genre> GetGenresByName(IEnumerable<string> names)
         => _dbSet.Where(g => names.Contains(g.Name)).ToList();
    
    }
}
