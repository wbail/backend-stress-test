using BackendStressTest.Infrastructure.Data.Repositories;
using BackendStressTest.Models;

namespace BackendStressTest.Services.Implementation
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<int> CountPeople()
        {
            return await _personRepository.CountPeople();
        }

        public async Task<Person> CreatePerson(Person person)
        {
            return await _personRepository.CreatePerson(person);
        }

        public async Task<IEnumerable<Person>> GetPeopleBySearchTerm(string searchTerm)
        {
            return await _personRepository.GetPeopleBySearchTerm(searchTerm);
        }

        public async Task<Person> GetPersonById(Guid id)
        {
            return await _personRepository.GetPersonById(id);
        }

        public async Task<bool> NicknameExists(string nickname)
        {
            return await _personRepository.NicknameExists(nickname);
        }
    }
}
