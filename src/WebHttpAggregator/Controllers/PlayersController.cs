using Microsoft.AspNetCore.Mvc;
using Players.Application.Commands;
using System.Net;
using WebHttpAggregator.Dtos;
using WebHttpAggregator.Services.Players;

namespace WebHttpAggregator.Controllers
{
    [Route("api/players")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayersService _playersService;

        public PlayersController(IPlayersService playersService)
        {
            _playersService = playersService;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(ICollection<PlayerDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ICollection<PlayerDto>>> GetPlayersAsync([FromQuery] int? teamId)
        {
            return await _playersService.GetPlayersAsync(teamId);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(PlayerDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PlayerDto>> CreatePlayerAsync([FromBody] PlayerCreateRequest request, [FromHeader(Name = "x-requestid")] string requestid)
        {
            Guid commandGuid;
            if (Guid.TryParse(requestid, out commandGuid))
            {
                return await _playersService.CreatePlayerAsync(request);
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
            return await _playersService.GetPlayerAsync(playerId);
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
                return await _playersService.SetPlayerTeamAsync(playerId, teamId);
            }
            else
            {
                return BadRequest("'x-requestid' header is not set.");
            }
        }
    }
}
