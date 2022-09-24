namespace TvMaze.Api.Controllers.Dto
{
  public class ActorDto
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime? Birthday { get; set; }
  }

  public class ShowDto
  {

    public int Id { get; set; }

    public string Name { get; set; }

    public List<ActorDto> Cast { get; set; }
  }
}
