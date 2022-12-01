using Microsoft.AspNetCore.Mvc;
using TeamsService.Services;
using Dtos;
using MassTransit;
using KafkaMessageBroker.Events;

namespace TeamsService.Controllers;

[ApiController]
[Route("api/teams")]
public class TeamsController : ControllerBase
{
    private readonly ITeamsService _teamsService;
    private readonly IPlayersDataClient _playersDataClient;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ITopicProducer<string, TeamRosterUpdateEvent> _kafkaTopicProducer;

    public TeamsController(
        ITeamsService teamsService, 
        IPlayersDataClient playersDataClient,
        IPublishEndpoint publishEndpoint,
        ITopicProducer<string, TeamRosterUpdateEvent> kafkaTopicProducer)
    {
        _teamsService = teamsService;
        _playersDataClient = playersDataClient;
        _publishEndpoint = publishEndpoint;
        _kafkaTopicProducer = kafkaTopicProducer;
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

    [HttpPut]
    [Route("{id:int}/players")]
    public async Task<ActionResult> UpdateTeamRosterAsync([FromRoute]int id, [FromBody]TeamRosterDto.UpdateData data)
    {
        data = data ?? new TeamRosterDto.UpdateData();
        data.TeamId = id;

        var message = new TeamRosterUpdateEvent(data);

        await _kafkaTopicProducer.Produce(Guid.NewGuid().ToString(), message);

        return Ok();
    }
}
