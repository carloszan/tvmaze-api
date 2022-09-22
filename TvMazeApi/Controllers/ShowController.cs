using Microsoft.AspNetCore.Mvc;
using TvMazeApi.Controllers.Dto;

namespace TvMazeApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ShowController
  {
    private readonly ILogger<ShowController> _logger;

    public ShowController(ILogger<ShowController> logger)
    {
      _logger = logger; 
    }

    [HttpGet]
    public IEnumerable<ShowDto> Get()
    {
      var cast = new List<ActorDto>() { new ActorDto { Name = "Carlos", Id = 1, Birthday = new DateTime(1999,1,1) } };
      return new List<ShowDto>() { new ShowDto() { Name = "Game of thrones", Id = 1, Cast = cast } };
    }
  }
}
