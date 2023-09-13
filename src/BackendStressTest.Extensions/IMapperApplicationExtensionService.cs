using BackendStressTest.Messages.Requests;
using BackendStressTest.Messages.Responses;
using BackendStressTest.Models;

namespace BackendStressTest.Extensions
{
    public interface IMapperApplicationExtensionService
    {
        Person CreatePersonRequestToPerson(CreatePersonRequest createPersonRequest);
        CreatePersonRequest PersonToCreatePersonRequest(Person person);
        Person GetPersonResponseToPerson(GetPersonResponse getPersonResponse);
        GetPersonResponse PersonToGetPersonResponse(Person person);
        IEnumerable<GetPersonResponse> CollectionOfPersonToCollectionOfGetPeopleResponse(IEnumerable<Person> people);
        Person CreatePersonResponseToPerson(CreatePersonResponse createPersonResponse);
        CreatePersonResponse PersonToCreatePersonResponse(Person person);
    }
}
