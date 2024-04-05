using Movies.Data.Models;

namespace Movies.Data.Interfaces
{
    public interface IGenreRepository : IBaseRepository<Genre>
    {
        IList<Genre> GetGenresByName(IEnumerable<string> names);
    }
}
