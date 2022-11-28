namespace Domain.Models;

public class City : Entity<int>
{
    protected City() 
    {
    }

    public City(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Name { get; private set; }

    public ICollection<Team> Teams { get; private set; }
}

