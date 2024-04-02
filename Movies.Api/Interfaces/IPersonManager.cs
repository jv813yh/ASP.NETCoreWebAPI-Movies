using Movies.Api.DTOs;

namespace Movies.Api.Interfaces
{
    public interface IPersonManager
    {
        Task<IList<PersonDTO>> GetAllPeopleAsync();

        /*
        
        Task<PersonDTO?> FindByIdAsync(uint id);
        
        Task<PersonDTO> InsertAsync(PersonDTO personDTO);
        
        Task<PersonDTO> UpdateAsync(PersonDTO personDTO);
        
        Task DeleteAsync(uint id);
        
        Task<bool> ExistsWithId(uint id);
        */
    }
}
