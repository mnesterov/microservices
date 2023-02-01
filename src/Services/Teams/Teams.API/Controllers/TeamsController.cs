using Microsoft.AspNetCore.Mvc;
using Teams.Dtos;
using Teams.Application.Queries;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace Teams.API.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/teams")]
public class TeamsController : ControllerBase
{
    private readonly ITeamsQueryService _teamsQueryService;

    public TeamsController(
        ITeamsQueryService teamsQueryService
        )
    {
        _teamsQueryService = teamsQueryService;
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(ICollection<TeamDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ICollection<TeamDto>>> GetTeamsAsync()
    {
        return Ok(await _teamsQueryService.GetTeamsAsync());
    }

    [HttpGet]
    [Route("{teamId:int}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(TeamDto), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<TeamDto>> GetTeamAsync(int teamId)
    {
        var team = await _teamsQueryService.GetTeamAsync(teamId);
        if (team != null)
        {
            return Ok(team);
        }
        else
        {
            return NotFound();
        }
    }
}
