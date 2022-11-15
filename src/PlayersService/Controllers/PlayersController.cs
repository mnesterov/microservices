using Microsoft.AspNetCore.Mvc;
using Dtos;
using PlayersService.Services;

namespace PlayersService.Controllers;

[ApiController]
[Route("api/players")]
public class PlayersController : ControllerBase
{
    private readonly IPlayersService _playersService;

    public PlayersController(IPlayersService playersService)
    {
        _playersService = playersService;
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult<ICollection<PlayerDto>>> GetPlayersAsync([FromQuery(Name = "teamid")]int? teamId)
    {
        return Ok(await _playersService.GetPlayersAsync(teamId));
    }
}
