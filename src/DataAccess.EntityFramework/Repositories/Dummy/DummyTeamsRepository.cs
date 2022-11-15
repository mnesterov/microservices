using Domain.Models;
using Domain.Repositories;

namespace DataAccess.EntityFramework.Repositories;

public class DummyTeamsRepository : ITeamsRepository
{
    private readonly Lazy<List<Team>> _teams;

    public DummyTeamsRepository() 
    {
        var city = new City(12, "Los Angeles");

        _teams = new Lazy<List<Team>>(() =>
        {
            return new List<Team>() 
            {
                new Team(5, "Clippers", city),
                new Team(14, "Lakers", city),
            };
        });
    }

    public async Task<ICollection<Team>> GetTeamsAsync() 
    {
        var task = Task.Run<List<Team>>(() =>
        {
            Thread.Sleep(1000);
            return _teams.Value;
        });

        return await task;
    }

    public async Task<Team> GetTeamAsync(int id)
    {
        var teamValue = await GetTeamsAsync();
        var team = teamValue.FirstOrDefault(t => t.Id == id);
        return team;
    }
}
