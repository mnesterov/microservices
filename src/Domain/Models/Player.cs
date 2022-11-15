namespace Domain.Models;

public class Player 
{
    protected Player()
    {
    }

    public Player(int id, DateTime birthday, string firstName, string lastName, int teamId)
    {
        Id = id;
        Birthday = birthday;
        FirstName = firstName;
        LastName = lastName;
        TeamId = teamId;
    }

    public int Id { get; private set; }
    public DateTime Birthday { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public int? TeamId { get; private set; }

    public Team Team { get ; private set; }
}