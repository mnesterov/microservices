using Players.Domain.Models.PlayerAggregate;

namespace Players.Domain.Events
{
    public class ChangePlayerTeamDomainEvent : IDomainEvent
    {
        public ChangePlayerTeamDomainEvent(Player player, int? newTeamId, int? oldTeamId)
        {
            Player = player;
            NewTeamId = newTeamId;
            OldTeamId = oldTeamId;
        }

        public int? NewTeamId { get; private set; }
        public int? OldTeamId { get; private set; }
        public Player Player { get; private set; }
    }
}
