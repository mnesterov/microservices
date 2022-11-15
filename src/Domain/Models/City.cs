namespace Domain.Models;

public class City 
{
    protected City() 
    {
    }

    public City(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }

    public ICollection<Team> Teams { get; private set; }
}

