using Movies.Api.DTOs;
using Movies.Data.Models;

namespace Movies.Api.Interfaces
{
    public interface IPersonManager
    {
        // Async method to get all people from the database and return them
        Task<IList<PersonDTO>> GetAllPeopleAsync();

        // Async method to get a person by id from the database and return it
        Task<PersonDTO?> GetPersonByIdAsync(uint id);

        // Async method to get people according to personRole and page and pageSize from the database and return them
        Task<IList<PersonDTO>> GetAllPeopleAsync(PersonRole personRole, int page, int pageSize);

        // Async method to add a person to the database and return it
        Task<PersonDTO> AddPersonAsync(PersonDTO personDTO);

        // Method to get a person by id from the database and return it
        PersonDTO? GetPersonById(uint id);

        // Async method to delete a person by id from the database and return it
        Task<PersonDTO?> DeletePersonAsync(uint id);

        // Async method to update a person by id from the database and return it
        Task<PersonDTO?> UpdatePersonAsync(uint id, PersonDTO personDto);
    }
}
