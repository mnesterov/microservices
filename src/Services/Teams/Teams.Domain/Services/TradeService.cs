using MediatR;
using Teams.Domain.Events;
using Teams.Domain.Models.TeamAggregate;
using Teams.Domain.Repositories;

namespace Teams.Domain.Services
{
    public class TradeService : ITradeService
    {
        private readonly ITeamsRepository _teamsRepository;
        private readonly ITradeValidator _tradeValidator;
        private readonly IMediator _mediator;

        public TradeService(
            ITeamsRepository teamsRepository, 
            IMediator mediator,
            ITradeValidator tradeValidator)
        {
            _teamsRepository = teamsRepository;
            _mediator = mediator;
            _tradeValidator = tradeValidator;
        }

        public async Task ProcessTrade(TradeData data, CancellationToken cancellationToken)
        {
            var result = await _tradeValidator.ValidateTrade(data);
            if (!result.IsValid)
            {
                await SendTeamSalaryBasketOverflowDomainEvent(result.Team, data);
                return;
            }

            var newTeamId = data.NewTeamId;
            if (newTeamId.HasValue)
            {
                var team = await _teamsRepository.GetTeamAsync(newTeamId.Value);
                team.UpdateSalaryBasket(data.PlayerSalary);
                _teamsRepository.UpdateTeam(team);
            }

            var oldTeamId = data.OldTeamId;
            if (oldTeamId.HasValue)
            {
                var team = await _teamsRepository.GetTeamAsync(oldTeamId.Value);
                team.UpdateSalaryBasket(-data.PlayerSalary);
                _teamsRepository.UpdateTeam(team);
            }

            await _teamsRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

        private async Task SendTeamSalaryBasketOverflowDomainEvent(Team team, TradeData data)
        {
            var domainEvent = new TeamSalaryBasketOverflowDomainEvent(team, data.OldTeamId, data.PlayerId, data.PlayerSalary);
            await _mediator.Publish(domainEvent);
        }
    }
}
