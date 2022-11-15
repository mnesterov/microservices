using Dtos;

namespace TeamsService.Services;

public interface IPlayersDataClient
{
    Task<ICollection<PlayerDto>> GetTeamPlayersAsync(int teamId);
} 

    