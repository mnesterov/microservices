using Microsoft.AspNetCore.Mvc;
using TeamsService.Services;
using Dtos;

namespace TeamsService.Controllers;

[ApiController]
[Route("api/teams")]
public class TeamsController : ControllerBase
{
    private readonly ITeamsService _teamsService;
    private readonly IPlayersDataClient _playersDataClient;

    public TeamsController(ITeamsService teamsService, IPlayersDataClient playersDataClient)
    {
        _teamsService = teamsService;
        _playersDataClient = playersDataClient;
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
    public async Task<ActionResult<PlayerDto>> GetTeamPlayersAsync(int id)
    {
        return Ok(await _playersDataClient.GetTeamPlayersAsync(id));
    }
}
