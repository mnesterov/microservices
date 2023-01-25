using Microsoft.EntityFrameworkCore;
using Players.Domain.Common;
using Players.Domain.Models.PlayerAggregate;
using Players.Infrastructure;

namespace Players.Infrastructure.Repositories;

internal class PlayersRepository : IPlayersRepository
{
    private readonly DbSet<Player> _players;
    private readonly AppDbContext _dbContext;

    public IUnitOfWork UnitOfWork => _dbContext;

    public PlayersRepository(AppDbContext dbContext)
    {
        _players = dbContext.Set<Player>();
        _dbContext = dbContext;
    }

    public async Task<Player> GetPlayerAsync(int id)
    {
        return await _players.FindAsync(id);
    }

    public async Task<ICollection<Player>> GetPlayersAsync()
    {
        return await _players.ToListAsync();
    }

    public async Task<ICollection<Player>> GetPlayersByTeamAsync(int teamId)
    {
        var teams = await _players.Where(p => p.TeamId == teamId).ToListAsync();
        return teams;
    }

    public Player AddPlayer(Player player)
    {
        return _dbContext.Add(player).Entity;
    }

    public void UpdatePlayer(Player player)
    {
        _dbContext.Entry(player).State = EntityState.Modified;
    }
}
