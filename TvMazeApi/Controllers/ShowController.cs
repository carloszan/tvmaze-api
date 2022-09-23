using Microsoft.AspNetCore.Mvc;
using TvMazeApi.Controllers.Dto;
using TvMazeApi.Repositories;

namespace TvMazeApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ShowController
  {
    private readonly ILogger<ShowController> _logger;
    private readonly IShowRepository _showRepository;

    public ShowController(
      ILogger<ShowController> logger,
      IShowRepository showRepository
    )
    {
      _logger = logger; 
      _showRepository = showRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<ShowDto>> Get([FromQuery(Name = "page")] string page)
    {
      var id = 250 * Int32.Parse(page);
      var shows = await _showRepository.GetNext250DocumentsByIdAsync(id);

      // Uggly mapping objects but as
      // I don't want to install any external dependencies, it's ok!
      var showsDto = shows
        .Select(show => new ShowDto
        {
          Id = show.Id,
          Name = show.Name,
          Cast = show.Cast.Select(cast => new ActorDto
          {
            Id = cast.Id,
            Name = cast.Name,
            Birthday = cast.Birthday
          }).ToList(),
        }).ToList();

      return showsDto;
    }
  }
}
