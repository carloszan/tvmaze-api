using Microsoft.Extensions.Logging;
using Moq;
using StackExchange.Redis;
using TvMaze.Api.Controllers;
using TvMaze.Api.Entities;
using TvMaze.Api.Repositories;

namespace TvMaze.Tests.Controllers
{
  public class ShowControllerTest
  {
    [Test]
    public async Task GetNext250DocumentsByIdAsync_WithValidId_ReturnsShows()
    {
      // Arrange
      var loggerMock = new Mock<ILogger<ShowController>>();
      var showRepositoryMock = new Mock<IShowRepository>();

      var databaseRedisMock = new Mock<IDatabase>();

      var connectionMultiplexerMock = new Mock<IConnectionMultiplexer>();
      connectionMultiplexerMock.Setup(_ => _.GetDatabase(It.IsAny<int>(), default)).Returns(databaseRedisMock.Object);

      var lastId = 1;
      var fakeCast = new List<Actor> { new Actor { Id = 1, Name = "John Travolta", Birthday = new DateTime(1969, 1, 1) } };
      var fakeShows = new List<ShowEntity>() { new ShowEntity { Id = lastId, Name = "Game of Thrones", Cast = fakeCast } };

      showRepositoryMock
        .Setup(x => x.GetNext250DocumentsByIdAsync(It.IsAny<int>()))
        .ReturnsAsync(fakeShows);

      var showController = new ShowController(
        loggerMock.Object,
        connectionMultiplexerMock.Object,
        showRepositoryMock.Object);

      // Act
      var shows = await showController.Get(new Api.Controllers.Dto.GetParamsDto());

      // Assert
      Assert.That(shows.FirstOrDefault().Id, Is.EqualTo(lastId));
    }
  }
}
