using Microsoft.Extensions.Logging;
using Moq;
using TvMazeApi.Controllers;
using TvMazeApi.Entities;
using TvMazeApi.Repositories;

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

      var lastId = 1;
      var fakeCast = new List<Actor> { new Actor { Id = 1, Name = "John Travolta", Birthday = new DateTime(1969, 1, 1) } };
      var fakeShows = new List<ShowEntity>() { new ShowEntity { Id = lastId, Name = "Game of Thrones", Cast = fakeCast } };

      showRepositoryMock
        .Setup(x => x.GetNext250DocumentsByIdAsync(It.IsAny<int>()))
        .ReturnsAsync(fakeShows);

      var showController = new ShowController(
        loggerMock.Object, 
        showRepositoryMock.Object);

      // Act
      var shows = await showController.Get("0");

      // Assert
      Assert.That(shows.FirstOrDefault().Id, Is.EqualTo(lastId));
    }
  }
}
