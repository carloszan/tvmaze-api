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

    public async Task<List<ShowEntity>> GetNext250DocumentsByIdAsync(int page)
    {
      //var filter = Builders<ShowEntity>.Filter.And();

      //return await _showCollection.FindAsync(filter, options);
      throw new NotImplementedException();
    }
  }
}
