using Microsoft.AspNetCore.Mvc;
using TeamsService.Services;
using Dtos;
using MassTransit;

namespace TeamsService.Controllers;

[ApiController]
[Route("api/teams")]
public class TeamsController : ControllerBase
{
    private readonly ITeamsService _teamsService;
    private readonly IPlayersDataClient _playersDataClient;
    private readonly IPublishEndpoint _publishEndpoint;

    public TeamsController(
        ITeamsService teamsService, 
        IPlayersDataClient playersDataClient,
        IPublishEndpoint publishEndpoint)
    {
        _teamsService = teamsService;
        _playersDataClient = playersDataClient;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult<IEnumerable<TeamDto>>> GetTeamsAsync()
    {
        return Ok(await _teamsService.GetTeamsAsync());
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<TeamDto>> GetTeamAsync(int id)
    {
        return Ok(await _teamsService.GetTeamAsync(id));
    }

    [HttpGet]
    [Route("{id:int}/players")]
    public async Task<ActionResult<ICollection<PlayerDto>>> GetTeamPlayersAsync(int id)
    {
        return Ok(await _playersDataClient.GetTeamPlayersAsync(id));
    }

    [HttpPost]
    [Route("{id:int}/players")]
    public async Task<ActionResult> AddTeamPlayerAsync([FromRoute]int id, [FromBody]PlayerDto.CreateData data)
    {
        data = data ?? new PlayerDto.CreateData();
        data.TeamId = id;

        await _publishEndpoint.Publish(data);

        return Ok();
    }
}
