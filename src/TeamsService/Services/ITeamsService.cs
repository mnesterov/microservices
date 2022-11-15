using Dtos;

namespace TeamsService.Services;

public interface ITeamsService
{
    Task<ICollection<TeamDto>> GetTeamsAsync();
    Task<TeamDto> GetTeamAsync(int id);
} 

    