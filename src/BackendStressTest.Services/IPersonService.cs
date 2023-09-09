using BackendStressTest.Models;

namespace BackendStressTest.Services
{
    public interface IPersonService
    {
        Task<Person> CreatePerson(Person person);
        Task<Person> GetPersonById(Guid id);
        Task<IEnumerable<Person>> GetPeopleBySearchTerm(string searchTerm);
        Task<int> CountPeople();
        Task<bool> NicknameExists(string nickname);
    }
}
