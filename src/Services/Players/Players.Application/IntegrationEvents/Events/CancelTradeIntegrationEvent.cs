using System.Text.Json.Serialization;

namespace Players.Application.IntegrationEvents.Events
{
    public class CancelTradeIntegrationEvent : IntegrationEvent
    {
        [JsonConstructor]
        public CancelTradeIntegrationEvent(Guid eventId, DateTime created, int teamId, int playerId, int? oldTeamId, double teamSalarySpaceUnderCap)
            : base(eventId, created)
        {
            TeamId = teamId;
            PlayerId = playerId;
            OldTeamId = oldTeamId;
            TeamSalarySpaceUnderCap = teamSalarySpaceUnderCap;
        }

        public int TeamId { get; private set; }
        public int PlayerId { get; private set; }
        public int? OldTeamId { get; private set; }
        public double TeamSalarySpaceUnderCap { get; private set; }
    }
}
