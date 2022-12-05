namespace Domain.Models;

public class Team : Entity<int>
{
    protected Team() 
    {
    }

    public Team(int id, string name, int cityId)
    {
        Id = id;
        Name = name;
        CityId = cityId;
    }

    public string Name { get; private set; }
    public int CityId { get; private set; }
    public City City { get; private set; }

    public ICollection<Player> Players { get; private set; }
}

