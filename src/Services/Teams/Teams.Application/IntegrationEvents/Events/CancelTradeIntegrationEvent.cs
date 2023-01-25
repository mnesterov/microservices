namespace Teams.Application.IntegrationEvents.Events
{
    public class CancelTradeIntegrationEvent : IntegrationEvent
    {
        public CancelTradeIntegrationEvent()
            : base()
        {
        }

        public CancelTradeIntegrationEvent(int teamId, int playerId, int? oldTeamId, double teamSalarySpaceUnderCap)
            : base()
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
