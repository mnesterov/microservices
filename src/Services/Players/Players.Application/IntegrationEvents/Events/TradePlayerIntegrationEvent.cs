namespace Players.Application.IntegrationEvents.Events
{
    public class TradePlayerIntegrationEvent : IntegrationEvent
    {
        public TradePlayerIntegrationEvent() 
            : base()
        {
        }

        public TradePlayerIntegrationEvent(int playerId, int? newTeamId, int? oldTeamId, double playerContractAnnualSalary)
            : base()
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
