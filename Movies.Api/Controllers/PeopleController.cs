using Microsoft.AspNetCore.Mvc;
using Movies.Api.DTOs;
using Movies.Api.Interfaces;

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
    }
}
