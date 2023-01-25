using Teams.Domain.Common;
using Teams.Domain.Models.TeamAggregate;

namespace Teams.Domain.Repositories;

public interface ITeamsRepository : IRepository<Team>
{
    Task<ICollection<Team>> GetTeamsAsync();
    Task<Team> GetTeamAsync(int id);

    Team AddTeam(Team team);
    void UpdateTeam(Team team);
}
