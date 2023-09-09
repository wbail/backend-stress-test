using BackendStressTest.Models;

namespace BackendStressTest.Infrastructure.Data.Repositories
{
    public interface IPersonRepository
    {
        Task<Person> CreatePerson(Person person);
        Task<Person> GetPersonById(Guid id);
        Task<IEnumerable<Person>> GetPeopleBySearchTerm(string searchTerm);
        Task<int> CountPeople();
        Task<bool> NicknameExists(string nickname);
    }
}
