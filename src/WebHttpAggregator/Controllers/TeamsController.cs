using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebHttpAggregator.Dtos;
using WebHttpAggregator.Services;
using WebHttpAggregator.Services.Players;

namespace WebHttpAggregator.Controllers
{
    [Route("api/teams")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamsService _teamsService;
        private readonly IPlayersService _playersService;

        public TeamsController(
            ITeamsService teamsService,
            IPlayersService playersService
            )
        {
            _teamsService = teamsService;
            _playersService = playersService;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(ICollection<TeamDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ICollection<TeamDto>>> GetTeamsAsync()
        {
            return await _teamsService.GetTeamsAsync();
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(TeamDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<TeamDto>> GetTeamAsync(int id)
        {
            return await _teamsService.GetTeamAsync(id);
        }

        [HttpGet]
        [Route("{id:int}/players")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(TeamDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ICollection<PlayerDto>>> GetTeamPlayersAsync(int id)
        {
            return await _playersService.GetPlayersAsync(id);
        }
    }
}
