using BackendStressTest.Extensions;
using BackendStressTest.Messages.Requests;
using BackendStressTest.Messages.Responses;
using BackendStressTest.Services;
using Microsoft.Extensions.Logging;

namespace BackendStressTest.Application.Implementation
{
    public class PersonApplicationService : IPersonApplicationService
    {
        private readonly IPersonService _personService;
        private readonly ILogger<PersonApplicationService> _logger;

        public PersonApplicationService(IPersonService personService, ILogger<PersonApplicationService> logger)
        {
            _personService = personService;
            _logger = logger;
        }

        public async Task<int> CountPeople()
        {
            return await _personService.CountPeople();
        }

        public async Task<CreatePersonResponse> CreatePerson(CreatePersonRequest createPersonRequest)
        {
            try
            {
                var nicknameExists = await _personService.NicknameExists(createPersonRequest.Nickname!);

                if (nicknameExists)
                {
                    return null;
                }

                var personMapped = MapperExtension.CreatePersonRequestToPerson(createPersonRequest);

                personMapped.Id = Guid.NewGuid();

                var person = await _personService.CreatePerson(personMapped);

                return MapperExtension.PersonToCreatePersonResponse(person);
            }
            catch (Exception e)
            {
                _logger.LogError(message: "Error: ", exception: e);
                throw;
            }
        }

        public async Task<IEnumerable<GetPersonResponse>> GetPeopleBySearchTerm(string searchTerm)
        {
            try
            {
                var people = await _personService.GetPeopleBySearchTerm(searchTerm);

                if (people == null || !people.Any())
                {
                    return new List<GetPersonResponse>();
                }

                return MapperExtension.PersonToGetPeopleResponse(people);
            }
            catch (Exception e)
            {

                _logger.LogError(message: "Error: ", exception: e);
                throw;
            }
        }

        public async Task<GetPersonResponse> GetPersonById(Guid id)
        {
            try
            {
                var person = await _personService.GetPersonById(id);

                if (person == null)
                {
                    return null;
                }

                return MapperExtension.GetPersonResponseToPerson(person);

            }
            catch (Exception e)
            {
                _logger.LogError(message: "Error: ", exception: e);
                throw;
            }
        }
    }
}
