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
    }
}