using Microsoft.AspNetCore.Mvc;
using Movies.Api.DTOs;
using Movies.Api.Interfaces;
using Movies.Data.Models;

namespace Movies.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonManager _personManager;

        public PeopleController(IPersonManager personManager)
        {
            _personManager = personManager;
        }

        /// <summary>
        /// Async method to get all people from the database and return them as a list of PersonDTO objects on route /api/people
        /// </summary>
        /// <returns></returns>
        [HttpGet("people")]
        public async Task<ActionResult<IList<PersonDTO>>> GetAllPeopleAsync()
        {
            IList<PersonDTO>? listPerons =  await _personManager.GetAllPeopleAsync();

            if (listPerons == null)
            {
                return NotFound();
            }

            return Ok(listPerons);
        }

        /// <summary>
        /// Async method to get a person by id from the database and return it as a PersonDTO object on route /api/people/{_id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns> PersonDTO </returns>
        //[HttpGet("people/{_id}")]
        public async Task<ActionResult<PersonDTO>> GetPersonByIdAsync(uint _id)
        {
            PersonDTO? person = await _personManager.GetPersonByIdAsync(_id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }


        /// <summary>
        /// Async method to get all actors from the database and return them as a list of PersonDTO objects on route /api/actors
        /// </summary>
        /// <param name="limit"></param>
        ///// <returns></returns>
        [HttpGet("actors")]
        public async Task<ActionResult<IList<PersonDTO>>> GetActorsAsync(int limit = int.MaxValue)
        {
            IList<PersonDTO>? listActors = await _personManager.GetAllPeopleAsync(PersonRole.Actor, 0, limit);

            if (listActors == null)
            {
                return NotFound();
            }

            return Ok(listActors);
        }

        [HttpGet("people/{_id}")]
        public ActionResult<PersonDTO> GetPersonById(uint _id)
        {
            PersonDTO? person =  _personManager.GetPersonById(_id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        /// <summary>
        /// Async method to get all directors from the database and return them as a list of PersonDTO objects on route /api/directors
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("directors")]
        public async Task<ActionResult<IList<PersonDTO>>> GetDirectorsAsync(int limit = int.MaxValue)
        {
            IList<PersonDTO>? listActors = await _personManager.GetAllPeopleAsync(PersonRole.Director, 0, limit);

            if (listActors == null)
            {
                return NotFound();
            }

            return Ok(listActors);
        }

        /// <summary>
        /// Async method to add a person to the database and return it as a PersonDTO object on route /api/people
        /// </summary>
        /// <param name="personDTO"></param>
        /// <returns></returns>
        [HttpPost("people")]
        public async Task<ActionResult<PersonDTO>> AddNewPersonAsync([FromBody] PersonDTO personDTO)
        {
            PersonDTO newPerson = await _personManager.AddPersonAsync(personDTO);

            return CreatedAtAction(nameof(GetPersonById), new { _id = newPerson.PersonId }, newPerson);
        }

        /// <summary>
        /// Async method to delete a person by id from the database and return it as a PersonDTO object on route /api/people/{_id}
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>

        [HttpDelete("people/{_id}")]
        public async Task<ActionResult<PersonDTO>> DeletePerson(uint _id)
        {
            PersonDTO? person = await _personManager.DeletePersonAsync(_id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }


        /// <summary>
        /// Async method to update a person by id from the database and return it as a PersonDTO object on route /api/people/{_id}
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="personDTO"></param>
        /// <returns></returns>
        [HttpPut("people/{_id}")]
        public async Task<ActionResult<PersonDTO>> UpdatePerson(uint _id, [FromBody] PersonDTO personDTO)
        {
            PersonDTO? updatedPerson = await _personManager.UpdatePersonAsync(_id, personDTO);

            if (updatedPerson == null)
            {
                return NotFound();
            }

            return Ok(updatedPerson);
        }
    }
}
