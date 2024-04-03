using AutoMapper;
using Movies.Api.DTOs;
using Movies.Api.Interfaces;
using Movies.Data.Interfaces;
using Movies.Data.Models;

namespace Movies.Api.Managers
{
    public class PersonManager : IPersonManager
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

       public PersonManager(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }


        // Async method to get all people from the database and return them
        // as a list of PersonDTO objects using the AutoMapper library 
        public async Task<IList<PersonDTO>> GetAllPeopleAsync()
        {
            IList<Person> peopleList = await _personRepository.GetAllAsync();

            return _mapper.Map<IList<PersonDTO>>(peopleList);
        }

        // Async method to get a person by id from the database and return it
        public async Task<PersonDTO?> GetPersonByIdAsync(uint id)
        {
            Person? person = await _personRepository.FindByIdAsync(id);

            if (person == null)
            {
                return null;
            }

            return _mapper.Map<PersonDTO>(person);
        }

        public async Task<IList<PersonDTO>> GetAllPeopleAsync(PersonRole personRole, int page = 0, int pageSize = int.MaxValue)
        {
            IList<Person> peopleList = await _personRepository.GetAllPeopleAsync(personRole, page, pageSize);

            return _mapper.Map<IList<PersonDTO>>(peopleList);
        }
    }
}
