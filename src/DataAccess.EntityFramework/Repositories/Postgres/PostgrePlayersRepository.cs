using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Domain.Repositories;

namespace DataAccess.EntityFramework.Repositories;

internal class PostgrePlayersRepository : IPlayersRepository
{
    private readonly DbSet<Player> _players;

    public PostgrePlayersRepository(AppDbContext dbContext) 
    {
        _players = dbContext.Set<Player>();
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
}
