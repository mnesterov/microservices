using System.Text.Json.Serialization;

namespace Teams.Application.IntegrationEvents.Events
{
    public class TradePlayerIntegrationEvent : IntegrationEvent
    {
        [JsonConstructor]
        public TradePlayerIntegrationEvent(Guid eventId, DateTime created, int playerId, int? newTeamId, int? oldTeamId, double playerContractAnnualSalary)
           : base(eventId, created)
        {
            PlayerId = playerId;
            NewTeamId = newTeamId;
            OldTeamId = oldTeamId;
            PlayerContractAnnualSalary = playerContractAnnualSalary;
        }

        public int PlayerId { get; private set; }
        public int? NewTeamId { get; private set; }
        public int? OldTeamId { get; private set; }
        public double PlayerContractAnnualSalary { get; private set; }
    }
}
