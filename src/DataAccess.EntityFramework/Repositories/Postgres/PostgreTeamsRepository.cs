using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Domain.Repositories;

namespace DataAccess.EntityFramework.Repositories;

internal class PostgreTeamsRepository : ITeamsRepository
{
    private readonly DbSet<Team> _teams;

    public PostgreTeamsRepository(AppDbContext dbContext) 
    {
        _teams = dbContext.Set<Team>();
    }

    public async Task<ICollection<Team>> GetTeamsAsync() 
    {
        return await _teams.Include(t => t.City).ToListAsync();
    }

    public async Task<Team> GetTeamAsync(int id)
    {
        var team = await _teams.Include(t => t.City).FirstOrDefaultAsync(t => t.Id == id);
        return team;
    }
}
