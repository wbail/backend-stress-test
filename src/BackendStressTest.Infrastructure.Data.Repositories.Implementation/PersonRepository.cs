using BackendStressTest.Infrastructure.Data.UnitOfWork;
using BackendStressTest.Models;
using Dapper;

namespace BackendStressTest.Infrastructure.Data.Repositories.Implementation
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DbSession _dbSession;
        private readonly IUnitOfWork _unitOfWork;

        public PersonRepository(DbSession dbSession, IUnitOfWork unitOfWork)
        {
            _dbSession = dbSession;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CountPeople()
        {
            var count = await _dbSession.Connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Persons");

            return count;
        }

        public async Task<Person> CreatePerson(Person person)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                await _dbSession.Connection.ExecuteAsync("INSERT INTO Persons VALUES (@Id, @Nickname, @Name, @Birthdate, @Stack)",
                    new
                    {
                        Id = person.Id,
                        Nickname = person.Nickname,
                        Name = person.Name,
                        Birthdate = person.Birthdate,
                        Stack = person.Stack
                    });

                _unitOfWork.Commit();

                return person;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<IEnumerable<Person>> GetPeopleBySearchTerm(string searchTerm)
        {
            var people = await _dbSession.Connection.QueryAsync<Person>(
                    @"SELECT p.Id, p.Name, p.Nickname, p.Birthdate, p.Stack
                    FROM persons p
                    WHERE p.Name ILIKE '%' || @Term || '%'
	                    OR p.Nickname ILIKE '%' || @Term || '%'
	                    OR @Term ILIKE SOME(p.Stack)
                    LIMIT 50;",
                    new { Term = searchTerm });

            return people;
        }

        public async Task<Person> GetPersonById(Guid id)
        {
            var person = await _dbSession.Connection.QueryFirstOrDefaultAsync<Person>(
                    @"SELECT * from Persons WHERE Id = @Id", new { Id = id });

            return person;
        }

        public async Task<bool> NicknameExists(string nickname)
        {
            var nicknameExists = await _dbSession.Connection.QueryFirstOrDefaultAsync<bool>(
                    @"SELECT 1 from Persons WHERE Nickname = @nickname", new { nickname = nickname });

            return nicknameExists;
        }
    }
}
