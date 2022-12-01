namespace Dtos;

public class TeamRosterDto 
{
    public int TeamId { get; set; }
    public ICollection<int> PlayersIds { get; set; }

    public class UpdateData
    {
        public int TeamId { get; set; }
        public ICollection<int> PlayersIds { get; set; }        
    }
}