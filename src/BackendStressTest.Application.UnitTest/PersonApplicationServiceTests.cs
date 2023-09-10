namespace BackendStressTest.Application.UnitTest
{
    public class PersonApplicationServiceTests
    {
        public PersonApplicationServiceTests()
        {
            _personServiceMock = new Mock<IPersonService>();
            _loggerMock = new Mock<ILogger<PersonApplicationService>>();
            _personApplicationService = new PersonApplicationService(_personServiceMock.Object, _loggerMock.Object);
        }

        private readonly PersonApplicationService _personApplicationService;
        private readonly Mock<IPersonService> _personServiceMock;
        private readonly Mock<ILogger<PersonApplicationService>> _loggerMock;

        [Fact]
        public async void PersonApplicationService_CountPeople_ReturnsZero()
        {
            int count = 0;

            _personServiceMock.Setup(x => x.CountPeople())
                .ReturnsAsync(count);

            int countPeople = await _personApplicationService.CountPeople();

            Assert.Equal(count, countPeople);
        }

        [Fact]
        public async void PersonApplicationService_CountPeople_ReturnsMoreThanZero()
        {
            int count = 1;

            _personServiceMock.Setup(x => x.CountPeople())
                .ReturnsAsync(count);

            int countPeople = await _personApplicationService.CountPeople();

            Assert.Equal(count, countPeople);
        }

        [Fact]
        public async void PersonApplicationService_GetPersonById_ReturnsPerson()
        {
            Guid id = Guid.Parse("324533D9-89C8-4993-85FD-90FF66403342");

            Person personMock = new Person
            {
                Id = id,
                Name = "Test",
                Nickname = "MyNicknameTest",
                Birthdate = DateOnly.Parse("1990-01-01")
            };

            _personServiceMock.Setup(x => x.GetPersonById(It.IsAny<Guid>()))
                .ReturnsAsync(personMock);

            GetPersonResponse getPersonResponse = await _personApplicationService.GetPersonById(id);

            Assert.NotNull(getPersonResponse);
            Assert.Equal(id, getPersonResponse.Id);
        }

        [Fact]
        public async void PersonApplicationService_GetPersonById_ReturnsNull()
        {
            Guid id = Guid.Parse("324533D9-89C8-4993-85FD-90FF66403342");

            _personServiceMock.Setup(x => x.GetPersonById(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);

            GetPersonResponse getPersonResponse = await _personApplicationService.GetPersonById(id);

            Assert.Null(getPersonResponse);
        }

        [Fact(Skip = "Pending abstraction from mapper")]
        public async void PersonApplicationService_CreatePerson_ReturnsPerson()
        {
            Guid id = Guid.Parse("6F9A7FC5-3D01-4E68-83CB-FDF19C2E0F61");

            CreatePersonRequest createPersonRequest = new CreatePersonRequest
            {
                Name = "Test",
                Nickname = "TestNickname",
                Birthdate = DateOnly.Parse("1990-01-01")
            };

            var person = new Person
            {
                Id = id,
                Name = "Test",
                Nickname = "TestNickname",
                Birthdate = DateOnly.Parse("1990-01-01")
            };

            _personServiceMock.Setup(x => x.CreatePerson(It.IsAny<Person>()))
                .ReturnsAsync(person);

            CreatePersonResponse createPersonResponse = await _personApplicationService.CreatePerson(createPersonRequest);

            Assert.NotNull(createPersonResponse);
        }

        [Fact(Skip = "Pending abstraction from mapper")]
        public async void PersonApplicationService_CreatePerson_ReturnsException()
        {
            //
        }

        [Fact]
        public async void PersonApplicationService_GetPeopleBySearchTerm_ReturnsZeroPeople()
        {
            string searchTerm = "Test";

            List<Person> peopleMock = new List<Person>();

            _personServiceMock.Setup(x => x.GetPeopleBySearchTerm(It.IsAny<string>()))
                .ReturnsAsync(peopleMock);

            IEnumerable<GetPersonResponse> people = await _personApplicationService.GetPeopleBySearchTerm(searchTerm);

            Assert.NotNull(people);
            Assert.Empty(people);
        }

        [Fact]
        public async void PersonApplicationService_GetPeopleBySearchTerm_ReturnsCollectionOfPeople()
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

            _personServiceMock.Setup(x => x.GetPeopleBySearchTerm(It.IsAny<string>()))
                .ReturnsAsync(peopleMock);

            IEnumerable<GetPersonResponse> people = await _personApplicationService.GetPeopleBySearchTerm(searchTerm);

            Assert.NotNull(people);
            Assert.NotEmpty(people);
            Assert.Equal(peopleMock.Count, people.Count());
        }
    }
}