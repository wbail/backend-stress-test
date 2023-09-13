using BackendStressTest.Messages.Requests;
using BackendStressTest.Messages.Responses;
using BackendStressTest.Models;

namespace BackendStressTest.Extensions
{
    public class MapperApplicationExtensionService : IMapperApplicationExtensionService
    {
        public Person CreatePersonRequestToPerson(CreatePersonRequest createPersonRequest)
        {
            return new Person
            {
                Name = createPersonRequest.Name,
                Nickname = createPersonRequest.Nickname,
                Birthdate = createPersonRequest.Birthdate,
                Stack = createPersonRequest.Stack
            };
        }

        public CreatePersonRequest PersonToCreatePersonRequest(Person person)
        {
            return new CreatePersonRequest
            {
                Name = person.Name,
                Nickname = person.Nickname,
                Birthdate = person.Birthdate,
                Stack = person.Stack
            };
        }

        public Person GetPersonResponseToPerson(GetPersonResponse getPersonResponse)
        {
            return new Person
            {
                Name = getPersonResponse.Name,
                Nickname = getPersonResponse.Nickname,
                Birthdate = getPersonResponse.Birthdate,
                Stack = getPersonResponse.Stack
            };
        }

        public GetPersonResponse PersonToGetPersonResponse(Person person)
        {
            return new GetPersonResponse
            {
                Id = person.Id,
                Name = person.Name,
                Nickname = person.Nickname,
                Birthdate = person.Birthdate,
                Stack = person.Stack
            };
        }

        public IEnumerable<GetPersonResponse> CollectionOfPersonToCollectionOfGetPeopleResponse(IEnumerable<Person> people)
        {
            var getPersonResponses = new List<GetPersonResponse>();

            foreach (Person person in people)
            {
                var newPerson = new GetPersonResponse
                {
                    Id = person.Id,
                    Name = person.Name,
                    Nickname = person.Nickname,
                    Birthdate = person.Birthdate,
                    Stack = person.Stack!
                };

                getPersonResponses.Add(newPerson);
            }

            return getPersonResponses;
        }

        public Person CreatePersonResponseToPerson(CreatePersonResponse createPersonResponse)
        {
            return new Person
            {
                Name = createPersonResponse.Name,
                Nickname = createPersonResponse.Nickname,
                Birthdate = createPersonResponse.Birthdate,
                Stack = createPersonResponse.Stack
            };
        }

        public CreatePersonResponse PersonToCreatePersonResponse(Person person)
        {
            return new CreatePersonResponse
            {
                Id = person.Id,
                Name = person.Name,
                Nickname = person.Nickname,
                Birthdate = person.Birthdate,
                Stack = person.Stack
            };
        }
    }
}
