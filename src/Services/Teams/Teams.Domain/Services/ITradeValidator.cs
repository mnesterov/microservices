using Teams.Domain.Models.TeamAggregate;

namespace Teams.Domain.Services
{
    public interface ITradeValidator
    {
        Task<ValidationResult> ValidateTrade(TradeData data);
    }

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public Team Team { get; set; }
    }
}
