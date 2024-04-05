using Movies.Data.Models;

namespace Movies.Data.Interfaces
{
    public interface IPersonRepository: IBaseRepository<Person>
    {
        Task<IList<Person>> GetAllPeopleAsync(PersonRole personRole, int page, int pageSize);

        IList<Person> GetPersonsByIds(IEnumerable<uint> personIds);
    }
}
