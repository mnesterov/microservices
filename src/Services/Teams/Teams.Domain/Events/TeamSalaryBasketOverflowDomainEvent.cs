using Teams.Domain.Models.TeamAggregate;

namespace Teams.Domain.Events
{
    public class TeamSalaryBasketOverflowDomainEvent : IDomainEvent
    {
        public TeamSalaryBasketOverflowDomainEvent(Team team, int? oldTeamId, int playerId, double playerSalary)
        {
            Team = team;
            OldTeamId = oldTeamId;
            PlayerId = playerId;
            PlayerSalary = playerSalary;
        }

        public Team Team { get; private set; }
        public int? OldTeamId { get; private set; }
        public int PlayerId { get; private set; }
        public double PlayerSalary { get; private set; }
    }
}
