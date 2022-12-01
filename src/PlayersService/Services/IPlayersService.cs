using Dtos;

namespace PlayersService.Services;

public interface IPlayersService
{
    Task<ICollection<PlayerDto>> GetPlayersAsync(int? teamId);
    Task<PlayerDto> CreatePlayerAsync(PlayerDto.CreateData data);
    Task UpdatePlayersTeamsAsync(TeamRosterDto.UpdateData data);
} 

    