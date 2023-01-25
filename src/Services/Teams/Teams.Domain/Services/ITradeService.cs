namespace Teams.Domain.Services
{
    public interface ITradeService
    {
        Task ProcessTrade(TradeData data, CancellationToken cancellationToken);
    }

    public class TradeData
    {
        public int? NewTeamId { get; set; }
        public int? OldTeamId { get; set; }
        public int PlayerId { get; set; }
        public double PlayerSalary { get; set; }
    }
}
