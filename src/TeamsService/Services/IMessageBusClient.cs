using Dtos;

namespace TeamsService.Services;

public interface IMessageBusClient
{
    void PublishCreateNewPlayer(PlayerDto.CreateData data);
}