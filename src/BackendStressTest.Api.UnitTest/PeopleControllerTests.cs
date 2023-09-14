using BackendStressTest.Messages.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
    }
}