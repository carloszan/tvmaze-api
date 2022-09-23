using MongoDB.Driver;
using Moq;
using TvMazeApi.Entities;
using TvMazeApi.Repositories;

namespace TvMazeTests.Repositories
{
  public class ShowRepositoryTest
  {
    [Test]
    public async Task GetNext250DocumentsByIdAsync_WithValidId_ReturnsShows()
    {
      // Arrange
      var lastId = 1;
      var fakeShows = new List<ShowEntity>() { new ShowEntity { Id = lastId, Name = "Game of Thrones", Cast = null } };

      Mock<IMongoClient> mongoClientMock = MongoClientFactory(fakeShows);

      var showRepository = new ShowRepository(mongoClientMock.Object);

      // Act
      var shows = await showRepository.GetNext250DocumentsByIdAsync(0);

      // Assert
      Assert.That(shows.FirstOrDefault().Id, Is.EqualTo(lastId));
    }
    private static Mock<IMongoClient> MongoClientFactory(List<ShowEntity> fakeShows)
    {
      var showCollection = new Mock<IMongoCollection<ShowEntity>>();
      showCollection.Object.InsertMany(fakeShows);

      var mockCursor = new Mock<IAsyncCursor<ShowEntity>>();
      mockCursor.Setup(x => x.Current).Returns(fakeShows);
      mockCursor.SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

      showCollection.Setup(x => x.FindAsync(
        It.IsAny<FilterDefinition<ShowEntity>>(),
        It.IsAny<FindOptions<ShowEntity, ShowEntity>>(),
        It.IsAny<CancellationToken>()
        )
      ).ReturnsAsync(mockCursor.Object);

      var dbMock = new Mock<IMongoDatabase>();
      dbMock.Setup(_ => _.GetCollection<ShowEntity>(It.IsAny<string>(), default)).Returns(showCollection.Object);

      var mongoClientMock = new Mock<IMongoClient>();
      mongoClientMock.Setup(_ => _.GetDatabase(It.IsAny<string>(), default)).Returns(dbMock.Object);
      return mongoClientMock;
    }
  }
}
