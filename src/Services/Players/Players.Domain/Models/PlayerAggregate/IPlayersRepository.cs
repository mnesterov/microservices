using Players.Domain.Common;

namespace Players.Domain.Models.PlayerAggregate
{
    public interface IPlayersRepository : IRepository<Player>
    {
        Task<Player> GetPlayerAsync(int id);
        Task<ICollection<Player>> GetPlayersAsync();
        Task<ICollection<Player>> GetPlayersByTeamAsync(int teamId);
        Player AddPlayer(Player player);
        void UpdatePlayer(Player player);
    }
}
