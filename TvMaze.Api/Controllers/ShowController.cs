using Microsoft.AspNetCore.Mvc;
using TvMaze.Api.Controllers.Dto;
using TvMaze.Api.Repositories;

namespace TvMaze.Api.Controllers
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

    [HttpGet("/shows")]
    public async Task<IEnumerable<ShowDto>> Get([FromQuery] GetParamsDto getParams)
    {
      var id = 250 * Int32.Parse(getParams.Page);
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
