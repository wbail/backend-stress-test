using BackendStressTest.Messages.Requests;
using BackendStressTest.Messages.Responses;
using BackendStressTest.Models;
using System;
using System.Collections;
using System.Collections.Immutable;

namespace BackendStressTest.Extensions
{
    public static class MapperExtension
    {
        public static Person CreatePersonRequestToPerson(this CreatePersonRequest createPersonRequest)
        {
            return new Person
            {
                Name = createPersonRequest.Name,
                Nickname = createPersonRequest.Nickname,
                Birthdate = createPersonRequest.Birthdate,
                Stack = createPersonRequest.Stack
            };
        }

        public static CreatePersonRequest CreatePersonRequestToPerson(this Person person)
        {
            return new CreatePersonRequest
            {
                Name = person.Name,
                Nickname = person.Nickname,
                Birthdate = person.Birthdate,
                Stack = person.Stack
            };
        }

        public static Person GetPersonResponseToPerson(this GetPersonResponse getPersonResponse)
        {
            return new Person
            {
                Name = getPersonResponse.Name,
                Nickname = getPersonResponse.Nickname,
                Birthdate = getPersonResponse.Birthdate,
                Stack = getPersonResponse.Stack
            };
        }

        public static GetPersonResponse GetPersonResponseToPerson(this Person person)
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

        public static IEnumerable<GetPersonResponse> PersonToGetPeopleResponse(this IEnumerable<Person> people)
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

        public static Person CreatePersonResponseToPerson(this CreatePersonResponse createPersonResponse)
        {
            return new Person
            {
                Name = createPersonResponse.Name,
                Nickname = createPersonResponse.Nickname,
                Birthdate = createPersonResponse.Birthdate,
                Stack = createPersonResponse.Stack
            };
        }

        public static CreatePersonResponse PersonToCreatePersonResponse(this Person person)
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
