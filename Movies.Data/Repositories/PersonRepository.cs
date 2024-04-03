using Microsoft.EntityFrameworkCore;
using Movies.Data.DbContexts;
using Movies.Data.Interfaces;
using Movies.Data.Models;

namespace Movies.Data.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(MoviesDbContext dbContext) : base(dbContext)
        {
        }

        // Async method to get all people from the database with a specific role, with page and pageSize and return them
        public async Task<IList<Person>> GetAllPeopleAsync(PersonRole personRole, int page, int pageSize)
         => await _dbSet.Where(p => p.Role == personRole)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}
