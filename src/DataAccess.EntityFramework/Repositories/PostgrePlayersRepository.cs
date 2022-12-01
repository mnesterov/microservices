using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Domain.Repositories;

namespace DataAccess.EntityFramework.Repositories;

internal class PostgrePlayersRepository : IPlayersRepository
{
    private readonly DbSet<Player> _players;
    private readonly AppDbContext _dbContext; 

    public PostgrePlayersRepository(AppDbContext dbContext) 
    {
        _players = dbContext.Set<Player>();
        _dbContext = dbContext;
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

    public async Task<Player> CreatePlayerAsync(Player.CreateData createData)
    {
        var player = CreatePlayerPrivate(createData);
        
        var entry = await _players.AddAsync(player);
        await _dbContext.SaveChangesAsync();
        
        return entry.Entity;
    }

    public async Task UpdateTeamForPlayersAsync(ICollection<int> playerIds, int? teamId)
    {
        var players = _players.Where(p => playerIds.Contains(p.Id));
        
        foreach (var p in players)
        {
            p.AssignToTeam(teamId);
        }
        
        await _dbContext.SaveChangesAsync();
    }

    #region Private Methods

    private Player CreatePlayerPrivate(Player.CreateData createData)
    {
        createData.Birthday = DateTime.SpecifyKind(createData.Birthday, DateTimeKind.Utc);

        var player = new Player(createData);
        return player;
    }

    #endregion
}
