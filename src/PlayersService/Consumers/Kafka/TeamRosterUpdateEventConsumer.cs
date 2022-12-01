using KafkaMessageBroker.Events;
using MassTransit;
using PlayersService.Services;

namespace PlayersService.Consumers;

public class TeamRosterUpdateEventConsumer : IConsumer<TeamRosterUpdateEvent>
{
    private readonly IPlayersService _playersService;

    public TeamRosterUpdateEventConsumer(IPlayersService playersService)
    {
        _playersService = playersService;
    }

    public async Task Consume(ConsumeContext<TeamRosterUpdateEvent> context)
    {
        await _playersService.UpdatePlayersTeamsAsync(context.Message.Data);
    }
}