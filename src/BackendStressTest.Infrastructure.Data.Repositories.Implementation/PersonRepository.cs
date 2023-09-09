using BackendStressTest.Models;
using Dapper;
using System.Data;

namespace BackendStressTest.Infrastructure.Data.Repositories.Implementation
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IDbConnection _dbConnection;

        public PersonRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> CountPeople()
        {
            
            using (_dbConnection)
            {
                _dbConnection.Open();

                var count = await _dbConnection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Persons");
                
                _dbConnection.Close();

                return count;
            }
        }

        public async Task<Person> CreatePerson(Person person)
        {
            using (_dbConnection)
            {
                _dbConnection.Open();
                using var transaction = _dbConnection.BeginTransaction();

                try
                {
                    await _dbConnection.ExecuteAsync("INSERT INTO Persons VALUES (@Id, @Nickname, @Name, @Birthdate, @Stack)",
                    new
                    {
                        Id = person.Id,
                        Nickname = person.Nickname,
                        Name = person.Name,
                        Birthdate = person.Birthdate,
                        Stack = person.Stack
                    }, transaction: transaction);
                    
                    transaction.Commit();
                    
                    return person;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    _dbConnection.Close();
                }
            }
        }

        public async Task<IEnumerable<Person>> GetPeopleBySearchTerm(string searchTerm)
        {
            using (_dbConnection)
            {
                _dbConnection.Open();

                var people = await _dbConnection.QueryAsync<Person>(
                    @"SELECT p.Id, p.Name, p.Nickname, p.Birthdate, p.Stack
                    FROM persons p
                    WHERE p.Name ILIKE '%' || @Term || '%'
	                    OR p.Nickname ILIKE '%' || @Term || '%'
	                    OR @Term ILIKE SOME(p.Stack)
                    LIMIT 50;",
                    new { Term = searchTerm });
                
                _dbConnection.Close();

                return people;
            }
        }

        public async Task<Person> GetPersonById(Guid id)
        {
            using (_dbConnection)
            {
                _dbConnection.Open();

                var person = await _dbConnection.QueryFirstOrDefaultAsync<Person>(
                    @"SELECT * from Persons WHERE Id = @Id", new { Id = id});

                _dbConnection.Close();

                return person;
            }
        }

        public async Task<bool> NicknameExists(string nickname)
        {
            using (_dbConnection)
            {
                _dbConnection.Open();

                var nicknameExists = await _dbConnection.QueryFirstOrDefaultAsync<bool>(
                    @"SELECT 1 from Persons WHERE Nickname = @nickname", new { nickname = nickname });

                _dbConnection.Close();

                return nicknameExists;
            }
        }
    }
}
