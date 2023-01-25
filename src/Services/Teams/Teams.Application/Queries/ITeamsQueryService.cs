using Teams.Dtos;

namespace Teams.Application.Queries
{
    public interface ITeamsQueryService
    {
        Task<ICollection<TeamDto>> GetTeamsAsync();
        Task<TeamDto> GetTeamAsync(int id);
    }
}
