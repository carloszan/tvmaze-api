using TvMaze.Api.Entities;

namespace TvMaze.Api.Repositories
{
  public interface IShowRepository
  {
    Task<List<ShowEntity>> GetNext250DocumentsByIdAsync(int id);
  }
}
