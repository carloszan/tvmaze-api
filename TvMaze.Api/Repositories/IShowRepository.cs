using TvMazeApi.Entities;

namespace TvMazeApi.Repositories
{
  public interface IShowRepository
  {
    Task<List<ShowEntity>> GetNext250DocumentsByIdAsync(int id);
  }
}
