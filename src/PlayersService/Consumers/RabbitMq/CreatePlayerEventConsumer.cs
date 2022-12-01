using Dtos;
using MassTransit;
using PlayersService.Services;

namespace PlayersService.Consumers;

public class CreatePlayerEventConsumer : IConsumer<PlayerDto.CreateData>
{
    private readonly IPlayersService _playersService;

    public CreatePlayerEventConsumer(IPlayersService playersService)
    {
        _playersService = playersService;
    }

    public async Task Consume(ConsumeContext<PlayerDto.CreateData> context)
    {
        await _playersService.CreatePlayerAsync(context.Message);
    }
}