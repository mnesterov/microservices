using Microsoft.EntityFrameworkCore;
using Teams.Domain.Common;
using Teams.Domain.Models.TeamAggregate;
using Teams.Domain.Repositories;

namespace Teams.Infrastructure.Repositories;

internal class TeamsRepository : ITeamsRepository
{
    private readonly DbSet<Team> _teams;
    private readonly AppDbContext _dbContext;

    public IUnitOfWork UnitOfWork => _dbContext;

    public TeamsRepository(AppDbContext dbContext)
    {
        _teams = dbContext.Set<Team>();
        _dbContext = dbContext;
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

    public Team AddTeam(Team team)
    {
        return _dbContext.Add(team).Entity;
    }

    public void UpdateTeam(Team team)
    {
        _dbContext.Entry(team).State = EntityState.Modified;
    }
}
