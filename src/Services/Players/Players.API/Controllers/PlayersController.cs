using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Players.Application.Commands;
using Players.Application.Commands.IdentifiedCommand;
using Players.Application.Dtos;
using Players.Application.Queries;
using System.Net;

namespace PlayersService.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/players")]
public class PlayersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IPlayersQueryService _queryService;

    public PlayersController(IMediator mediator, IPlayersQueryService queryService)
    {
        _mediator = mediator;
        _queryService = queryService;
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(ICollection<PlayerDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ICollection<PlayerDto>>> GetPlayersAsync([FromQuery(Name = "teamid")]int? teamId)
    {
        var players = await _queryService.GetPlayersAsync(teamId);
        return Ok(players);
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(PlayerDto), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PlayerDto>> CreatePlayerAsync([FromBody]CreatePlayerCommand command, [FromHeader(Name = "x-requestid")] string requestid)
    {
        Guid commandGuid;
        if (Guid.TryParse(requestid, out commandGuid))
        {
            var identifiedCommand = new IdentifiedCommand<CreatePlayerCommand, PlayerDto>(command, commandGuid);
            var dto = await _mediator.Send(identifiedCommand);
            return Ok(dto);
        }
        else
        {
            return BadRequest("'x-requestid' header is not set.");
        }
    }

    [HttpGet]
    [Route("{playerId:int}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(PlayerDto), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PlayerDto>> GetPlayerAsync([FromRoute] int playerId)
    {
        var player = await _queryService.GetPlayerAsync(playerId);
        if (player != null)
        {
            return Ok(player);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPut]
    [Route("{playerId:int}/team")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult> SetPlayerTeamAsync([FromRoute] int playerId, [FromBody] int teamId, [FromHeader(Name = "x-requestid")] string requestid)
    {
        Guid commandGuid;
        if (Guid.TryParse(requestid, out commandGuid))
        {
            var command = new ChangePlayerTeamCommand(playerId, teamId);
            var identifiedCommand = new IdentifiedCommand<ChangePlayerTeamCommand, bool>(command, commandGuid);
            await _mediator.Send(identifiedCommand);
            return Ok();
        }
        else
        {
            return BadRequest("'x-requestid' header is not set.");
        }
    }
}
