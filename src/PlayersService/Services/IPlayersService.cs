using Dtos;

namespace PlayersService.Services;

public interface IPlayersService
{
    Task<ICollection<PlayerDto>> GetPlayersAsync(int? teamId);
    Task<PlayerDto> CreatePlayer(PlayerDto.CreateData data);
} 

    