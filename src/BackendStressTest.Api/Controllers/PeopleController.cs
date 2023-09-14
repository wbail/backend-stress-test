using BackendStressTest.Application;
using BackendStressTest.Messages.Requests;
using BackendStressTest.Messages.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace BackendStressTest.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonApplicationService _personApplicationService;

        public PeopleController(IPersonApplicationService personApplicationService)
        {
            _personApplicationService = personApplicationService;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreatePerson([FromBody] CreatePersonRequest createPersonRequest)
        {
            if (createPersonRequest.Validate())
            {
                var person = await _personApplicationService.CreatePerson(createPersonRequest);

                if (person == null) 
                {
                    return UnprocessableEntity(createPersonRequest);
                }

                return Created($"/people/{person.Id}", person);
            }

            return UnprocessableEntity(createPersonRequest);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetPersonResponse>> GetById(Guid id)
        {
            var person = await _personApplicationService.GetPersonById(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GetPersonResponse>>> GetBySearchTerm([FromQuery] string s)
        {
            var people = await _personApplicationService.GetPeopleBySearchTerm(s);

            return Ok(people);
        }

        [HttpGet("count-people")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> Count()
        {
            return Ok(await _personApplicationService.CountPeople());
        }
    }
}
