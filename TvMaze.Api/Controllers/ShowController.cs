using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using TvMaze.Api.Controllers.Dto;
using TvMaze.Api.Repositories;
using TvMaze.Api.Utils;

namespace TvMaze.Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ShowController
  {
    private readonly ILogger<ShowController> _logger;
    private readonly IDatabase _redis;
    private readonly IShowRepository _showRepository;

    public ShowController(
      ILogger<ShowController> logger,
      IConnectionMultiplexer redis,
      IShowRepository showRepository
    )
    {
      _logger = logger;
      _redis = redis.GetDatabase();
      _showRepository = showRepository;
    }

    [HttpGet("/shows")]
    public async Task<IEnumerable<ShowDto>> Get([FromQuery] GetParamsDto getParams)
    {
      var id = 250 * Int32.Parse(getParams.Page);
      var response = await _redis.GetAsync<List<ShowDto>>(id.ToString());

      if (response != null)
      {
        return response;
      }

      var shows = await _showRepository.GetNext250DocumentsByIdAsync(id);

      // Uggly mapping objects but as
      // I don't want to install any external dependencies, it's ok!
      var showsDto = shows
        .Select(show => new ShowDto
        {
          Id = show.Id,
          Name = show.Name,
          Cast = show.Cast?.Select(cast => new ActorDto
          {
            Id = cast.Id,
            Name = cast.Name,
            Birthday = cast.Birthday
          }).ToList(),
        }).ToList();

      // Caching response for 24 hours
      await _redis.SetAsync(id.ToString(), showsDto, new TimeSpan(24, 0, 0));

      return showsDto;
    }
  }
}
