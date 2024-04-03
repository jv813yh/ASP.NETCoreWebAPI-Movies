using Movies.Api.DTOs;
using Movies.Data.Models;

namespace Movies.Api.Interfaces
{
    public interface IPersonManager
    {
        Task<IList<PersonDTO>> GetAllPeopleAsync();

        Task<PersonDTO?> GetPersonByIdAsync(uint id);

        Task<IList<PersonDTO>> GetAllPeopleAsync(PersonRole personRole, int page, int pageSize);

        /*
        
        Task<PersonDTO> InsertAsync(PersonDTO personDTO);
        
        Task<PersonDTO> UpdateAsync(PersonDTO personDTO);
        
        Task DeleteAsync(uint id);
        
        Task<bool> ExistsWithId(uint id);
        */
    }
}
