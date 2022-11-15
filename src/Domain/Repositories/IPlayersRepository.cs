using Domain.Models;

namespace Domain.Repositories;

public interface IPlayersRepository 
{
    Task<ICollection<Player>> GetPlayersAsync();
    Task<ICollection<Player>> GetPlayersByTeamAsync(int TeamId);
}
