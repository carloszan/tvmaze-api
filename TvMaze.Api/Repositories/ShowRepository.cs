using MongoDB.Driver;
using TvMazeApi.Entities;

namespace TvMazeApi.Repositories
{
  public class ShowRepository : IShowRepository
  {

    private const string databaseName = "shows";
    private const string collectionName = "shows";
    private readonly IMongoCollection<ShowEntity> _showCollection;

    public ShowRepository(IMongoClient mongoClient)
    {
      IMongoDatabase database = mongoClient.GetDatabase(databaseName);
      _showCollection = database.GetCollection<ShowEntity>(collectionName);
    }

    public async Task<List<ShowEntity>> GetNext250DocumentsByIdAsync(int id)
    {
      var ids = new List<int>();

      for (var i = id; i < id + 250; i++)
      {
        ids.Add(i);
      }

      var shows = (await _showCollection
        .FindAsync(show => ids.Contains(show.Id))).ToList();

      return shows;
    }
  }
}
