namespace BackendStressTest.Api.UnitTest
{
    public class PeopleControllerTests
    {
        public PeopleControllerTests()
        {
            _personApplicationServiceMock = new Mock<IPersonApplicationService>();

            _peopleController = new PeopleController(_personApplicationServiceMock.Object);
        }

        private readonly Mock<IPersonApplicationService> _personApplicationServiceMock;
        private readonly PeopleController _peopleController;

        [Fact]
        public async void PeopleController_GetById_ReturnStatus200Ok()
        {
            Guid id = Guid.NewGuid();

            GetPersonResponse getPersonResponse = new GetPersonResponse
            {
                Id = id,
                Name = "Test",
                Nickname = "MyNicknameTest",
                Birthdate = DateOnly.Parse("1990-01-01")
            };

            _personApplicationServiceMock.Setup(x => x.GetPersonById(It.IsAny<Guid>()))
                .ReturnsAsync(getPersonResponse);

            var result = await _peopleController.GetById(id);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)okResult.StatusCode!);
            Assert.Equal(id, result.Value!.Id);
        }

        [Fact]
        public async void PeopleController_GetById_ReturnStatus404NotFound()
        {
            Guid id = Guid.NewGuid();

            _personApplicationServiceMock.Setup(x => x.GetPersonById(It.IsAny<Guid>()))
                .ReturnsAsync(() => null!);

            var result = await _peopleController.GetById(id);
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);

            Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)notFoundResult.StatusCode!);
        }

        [Fact]
        public async void PeopleController_CountPeople_Returns200Ok()
        {
            int count = 7;

            _personApplicationServiceMock.Setup(x => x.CountPeople())
                .ReturnsAsync(count);

            ActionResult<int> result = await _peopleController.Count();

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result.Result);
            var countPeople = okResult.Value;

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)okResult.StatusCode!);
            Assert.Equal(count, countPeople);
        }

        [Fact]
        public async void PeopleController_GetBySearchTerm_Returns200Ok()
        {
            string searchTerm = "test";

            List<GetPersonResponse> getPersonResponsesMock = new List<GetPersonResponse>
            {
                new GetPersonResponse
                {
                    Id = Guid.NewGuid(),
                    Name = "Test1",
                    Nickname = "Test1",
                    Birthdate = DateOnly.Parse("1991-01-01")
                },
                new GetPersonResponse
                {
                    Id = Guid.NewGuid(),
                    Name = "Test2",
                    Nickname = "Test2",
                    Birthdate = DateOnly.Parse("1992-02-02")
                },
                new GetPersonResponse
                {
                    Id = Guid.NewGuid(),
                    Name = "Test3",
                    Nickname = "Test3",
                    Birthdate = DateOnly.Parse("1993-03-03"),
                    Stack = ["c#", "azure", ".net"]
                },
            };

            _personApplicationServiceMock.Setup(x => x.GetPeopleBySearchTerm(It.IsAny<string>()))
                .ReturnsAsync(getPersonResponsesMock);

            ActionResult<IEnumerable<GetPersonResponse>> result = await _peopleController.GetBySearchTerm(searchTerm);
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result.Result);
            object? getPersonResponses = okResult.Value;

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)okResult.StatusCode!);
            Assert.NotNull(getPersonResponses);
        }

        [Fact]
        public async void PeopleController_CreatePerson_Returns201Created()
        {
            CreatePersonRequest createPersonRequest = new CreatePersonRequest
            {
                Name = "Test",
                Nickname = "Test",
                Birthdate = DateOnly.Parse("1990-01-01")
            };

            CreatePersonResponse createPersonResponseMock = new CreatePersonResponse
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Nickname = "Test",
                Birthdate = DateOnly.Parse("1990-01-01")
            };

            _personApplicationServiceMock.Setup(x => x.CreatePerson(It.IsAny<CreatePersonRequest>()))
                .ReturnsAsync(createPersonResponseMock);

            IActionResult result = await _peopleController.CreatePerson(createPersonRequest);

            CreatedResult createdResult = Assert.IsType<CreatedResult>(result);
            object? createPersonResponse = createdResult.Value;

            Assert.NotNull(createdResult);
            Assert.Equal(HttpStatusCode.Created, (HttpStatusCode)createdResult.StatusCode!);
            Assert.NotNull(createPersonResponse);
        }

        [Fact]
        public async void PeopleController_CreatePerson_RequestIsValid_Returns422UnprocessableEntity()
        {
            CreatePersonRequest createPersonRequest = new CreatePersonRequest
            {
                Name = "Test",
                Nickname = "Test",
                Birthdate = DateOnly.Parse("1990-01-01")
            };

            _personApplicationServiceMock.Setup(x => x.CreatePerson(It.IsAny<CreatePersonRequest>()))
                .ReturnsAsync(() => null);

            IActionResult result = await _peopleController.CreatePerson(createPersonRequest);

            UnprocessableEntityObjectResult unprocessableEntityObjectResult = Assert.IsType<UnprocessableEntityObjectResult>(result);
            object? createPersonResponse = unprocessableEntityObjectResult.Value;

            Assert.NotNull(unprocessableEntityObjectResult);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, (HttpStatusCode)unprocessableEntityObjectResult.StatusCode!);
            Assert.NotNull(createPersonResponse);
        }
    }
}