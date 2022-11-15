using Domain.Models;

namespace Domain.Repositories;

public interface ITeamsRepository 
{
    Task<ICollection<Team>> GetTeamsAsync();
    Task<Team> GetTeamAsync(int id);
}
