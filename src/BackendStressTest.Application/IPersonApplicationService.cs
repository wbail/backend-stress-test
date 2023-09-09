using BackendStressTest.Messages.Requests;
using BackendStressTest.Messages.Responses;

namespace BackendStressTest.Application
{
    public interface IPersonApplicationService
    {
        Task<CreatePersonResponse> CreatePerson(CreatePersonRequest createPersonRequest);
        Task<GetPersonResponse> GetPersonById(Guid id);
        Task<IEnumerable<GetPersonResponse>> GetPeopleBySearchTerm(string searchTerm);
        Task<int> CountPeople();
    }
}
