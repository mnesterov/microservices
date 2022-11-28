using Domain.Models;
using Domain.Repositories;

namespace DataAccess.DummyData.Repositories;

internal class DummyPlayersRepository : IPlayersRepository
{
    private readonly Lazy<List<Player>> _players;

    public DummyPlayersRepository() 
    {
        _players = new Lazy<List<Player>>(() => 
        {
            const int lakersTeamId = 14;

            return new List<Player>()
            {
                new Player(0, new DateTime(1978, 8, 23), "Kobe", "Bryant", lakersTeamId),
                new Player(1, new DateTime(1972, 3, 6), "Shaquille", "O'Neal", lakersTeamId),
                new Player(2, new DateTime(1974, 8, 9), "Derek", "Fisher", lakersTeamId),
                
                new Player(2, new DateTime(1979, 11, 3), "Elton", "Brand", 5),
            };
        });
    }

    public async Task<ICollection<Player>> GetPlayersAsync() 
    {
        var task = Task.Run<List<Player>>(() =>
        {
            Thread.Sleep(1000);
            return _players.Value;
        });

        return await task;
    }

    public async Task<ICollection<Player>> GetPlayersByTeamAsync(int teamId) 
    {
        var playersValue = await GetPlayersAsync();
        var players = playersValue.Where(p => p.TeamId == teamId).ToList();
        return players;
    }

    public async Task<Player> CreatePlayer(Player.CreateData createData)
    {
        throw new NotImplementedException();
    }
}
