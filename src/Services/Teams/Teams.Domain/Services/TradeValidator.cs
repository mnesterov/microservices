using Teams.Domain.Repositories;

namespace Teams.Domain.Services
{
    public class TradeValidator : ITradeValidator
    {
        private readonly ITeamsRepository _teamsRepository;

        public TradeValidator(ITeamsRepository teamsRepository)
        {
            _teamsRepository = teamsRepository;
        }

        public async Task<ValidationResult> ValidateTrade(TradeData data)
        {
            if (!data.NewTeamId.HasValue)
            {
                return new ValidationResult { IsValid = true };
            }

            var team = await _teamsRepository.GetTeamAsync(data.NewTeamId.Value);

            return new ValidationResult
            {
                IsValid = team.SpaceUnderSalaryCap >= data.PlayerSalary,
                Team = team
            };
        }
    }
}
