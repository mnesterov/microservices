namespace Domain.Models;

public class Player : Entity<int>
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

    public Player(CreateData data)
    {
        Birthday = data.Birthday;
        FirstName = data.FirstName;
        LastName = data.LastName;
        TeamId = data.TeamId;
    }

    public DateTime Birthday { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public int? TeamId { get; private set; }

    public Team Team { get ; private set; }

    #region Create Data

    public class CreateData
    {
        public DateTime Birthday { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? TeamId { get; set; }     
    }

    #endregion
}