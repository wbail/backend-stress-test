using BackendStressTest.Models;

namespace BackendStressTest.Services.UnitTest
{
    public class PersonServiceTests
    {
        public PersonServiceTests()
        {
            _personRepositoryMock = new Mock<IPersonRepository>();
            _personService = new PersonService(_personRepositoryMock.Object);
        }

        private readonly Mock<IPersonRepository> _personRepositoryMock;
        private PersonService _personService;

        [Fact]
        public async void PersonService_NicknameExists_ReturnsTrue()
        {
            string nickname = "MyNicknameAlreadyExists";

            _personRepositoryMock.Setup(x => x.NicknameExists(It.IsAny<string>()))
                .ReturnsAsync(true);

            bool nicknameExists = await _personService.NicknameExists(nickname);

            Assert.True(nicknameExists);
        }

        [Fact]
        public async void PersonService_NicknameExists_ReturnsFalse()
        {
            string nickname = "MyNicknameDontExists";

            _personRepositoryMock.Setup(x => x.NicknameExists(It.IsAny<string>()))
                .ReturnsAsync(false);

            bool nicknameExists = await _personService.NicknameExists(nickname);

            Assert.False(nicknameExists);
        }

        [Fact]
        public async void PersonService_CreatePerson_ReturnsAPerson()
        {
            Guid id = Guid.Parse("6F9A7FC5-3D01-4E68-83CB-FDF19C2E0F61");

            Person personMock = new Person
            {
                Id = id,
                Name = "Test",
                Nickname = "TestNickname",
                Birthdate = DateOnly.Parse("1990-01-01")
            };

            _personRepositoryMock.Setup(x => x.CreatePerson(It.IsAny<Person>()))
                .ReturnsAsync(personMock);

            Person person = await _personService.CreatePerson(personMock);

            Assert.NotNull(person);
            Assert.Equal(personMock, person);
        }

        [Fact]
        public async void PersonService_CreatePerson_ReturnsException()
        {
            Guid id = Guid.Parse("6F9A7FC5-3D01-4E68-83CB-FDF19C2E0F61");

            Person personMock = new Person
            {
                Id = id,
                Name = "Test",
                Nickname = "TestNickname",
                Birthdate = DateOnly.Parse("1990-01-01")
            };

            _personRepositoryMock.Setup(x => x.CreatePerson(It.IsAny<Person>()))
                .ThrowsAsync(new Exception("Error Message"));

            Exception exceptionThrown = await Assert.ThrowsAsync<Exception>(() => _personService.CreatePerson(personMock));

            Assert.Equal("Error Message", exceptionThrown.Message);
        }

        [Fact]
        public async void PersonService_GetPersonById_ReturnsPerson()
        {
            Guid id = Guid.Parse("6F9A7FC5-3D01-4E68-83CB-FDF19C2E0F61");

            Person personMock = new Person
            {
                Id = id,
                Name = "Test",
                Nickname = "TestNickname",
                Birthdate = DateOnly.Parse("1990-01-01")
            };

            _personRepositoryMock.Setup(x => x.GetPersonById(It.IsAny<Guid>()))
                .ReturnsAsync(personMock);

            Person person = await _personService.GetPersonById(id);

            Assert.NotNull(person);
            Assert.Equal(id, person.Id);
        }

        [Fact]
        public async void PersonService_GetPersonById_ReturnsNull()
        {
            Guid id = Guid.Parse("6F9A7FC5-3D01-4E68-83CB-FDF19C2E0F61");

            _personRepositoryMock.Setup(x => x.GetPersonById(It.IsAny<Guid>()))
                .ReturnsAsync(() => null!);

            Person person = await _personService.GetPersonById(id);

            Assert.Null(person);
        }

        [Fact]
        public async void PersonService_GetPeopleBySearchTerm_ReturnsCollectionOfPerson()
        {
            string searchTerm = "Test";

            List<Person> peopleMock = new List<Person>
            {
                new Person
                {
                    Id = Guid.NewGuid(),
                    Name = "Test1",
                    Nickname = "MyNicknameTest1",
                    Birthdate = DateOnly.Parse("1990-01-01"),
                    Stack = [".net", "c#", "docker"]
                },
                new Person
                {
                    Id = Guid.NewGuid(),
                    Name = "Test2",
                    Nickname = "MyNicknameTest2",
                    Birthdate = DateOnly.Parse("1990-02-02")
                },
                new Person
                {
                    Id = Guid.NewGuid(),
                    Name = "Test2",
                    Nickname = "MyNicknameTest2",
                    Birthdate = DateOnly.Parse("1990-03-03"),
                    Stack = ["oracle", "c#", "docker", "postgres"]
                }
            };

            _personRepositoryMock.Setup(x => x.GetPeopleBySearchTerm(It.IsAny<string>()))
                .ReturnsAsync(peopleMock);

            IEnumerable<Person> people = await _personService.GetPeopleBySearchTerm(searchTerm);

            Assert.NotNull(people);
            Assert.Equal(3, peopleMock.Count());
        }

        [Fact]
        public async void PersonService_GetPeopleBySearchTerm_ReturnsZeroPeople()
        {
            string searchTerm = "Test";

            List<Person> peopleMock = new List<Person>();

            _personRepositoryMock.Setup(x => x.GetPeopleBySearchTerm(It.IsAny<string>()))
                .ReturnsAsync(peopleMock);

            IEnumerable<Person> people = await _personService.GetPeopleBySearchTerm(searchTerm);

            Assert.NotNull(people);
            Assert.Empty(people);
        }

        [Fact]
        public async void PersonService_CountPeople_ReturnsZero()
        {
            int count = 0;

            _personRepositoryMock.Setup(x => x.CountPeople())
                .ReturnsAsync(count);

            int countPeople = await _personService.CountPeople();

            Assert.Equal(0, countPeople);
        }

        [Fact]
        public async void PersonService_CountPeople_ReturnsMoreThanZero()
        {
            int count = 1;

            _personRepositoryMock.Setup(x => x.CountPeople())
                .ReturnsAsync(count);

            int countPeople = await _personService.CountPeople();

            Assert.Equal(1, countPeople);
        }
    }
}