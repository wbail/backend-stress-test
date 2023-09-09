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

            _personRepositoryMock.Setup(x => x.NicknameExists(nickname))
                .ReturnsAsync(true);

            bool nicknameExists = await _personService.NicknameExists(nickname);

            Assert.True(nicknameExists);
        }

        [Fact]
        public async void PersonService_NicknameExists_ReturnsFalse()
        {
            string nickname = "MyNicknameDontExists";

            _personRepositoryMock.Setup(x => x.NicknameExists(nickname))
                .ReturnsAsync(false);

            bool nicknameExists = await _personService.NicknameExists(nickname);

            Assert.False(nicknameExists);
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

            _personRepositoryMock.Setup(x => x.GetPersonById(id))
                .ReturnsAsync(personMock);

            Person person = await _personService.GetPersonById(id);

            Assert.NotNull(person);
            Assert.Equal(person.Id, id);
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