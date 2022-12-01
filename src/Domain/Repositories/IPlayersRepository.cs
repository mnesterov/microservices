using Domain.Models;

namespace Domain.Repositories;

public interface IPlayersRepository 
{
    Task<ICollection<Player>> GetPlayersAsync();
    Task<ICollection<Player>> GetPlayersByTeamAsync(int teamId);
    Task<Player> CreatePlayerAsync(Player.CreateData createData);
    Task UpdateTeamForPlayersAsync(ICollection<int> playerIds, int? teamId);
}
