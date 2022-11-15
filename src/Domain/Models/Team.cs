namespace Domain.Models;

public class Team 
{
    protected Team() 
    {
    }

    public Team(int id, string name, City city)
    {
        Id = id;
        Name = name;
        CityId = city.Id;
        City = city;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public int CityId { get; private set; }
    public City City { get; private set; }

    public ICollection<Player> Players { get; private set; }
}

