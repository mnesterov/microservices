using Microsoft.Extensions.Logging;
using Teams.Application.DomainEventHandlers;
using Teams.Application.IntegrationEvents;
using Teams.Application.IntegrationEvents.Events;
using Teams.Domain.Events;

namespace Players.Application.DomainEventHandlers
{
    public class TeamSalaryBasketOverflowDomainEventHandler : IDomainEventHandler<TeamSalaryBasketOverflowDomainEvent>
    {
        private readonly IIntegrationEventPublishService _integrationEventPublishService;
        private readonly ILogger<TeamSalaryBasketOverflowDomainEventHandler> _logger;

        public TeamSalaryBasketOverflowDomainEventHandler(
            IIntegrationEventPublishService integrationEventPublishService,
            ILogger<TeamSalaryBasketOverflowDomainEventHandler> logger)
        {
            _integrationEventPublishService = integrationEventPublishService;
            _logger = logger;
        }

        public async Task Handle(TeamSalaryBasketOverflowDomainEvent n, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"--> Team {n.Team.Id} {n.Team.FullName} salary cap overflow after player {n.PlayerId} assigment. Salary rest under the cap is {n.Team.SpaceUnderSalaryCap}");

            var integrationEvent = new CancelTradeIntegrationEvent(n.Team.Id, n.PlayerId, n.OldTeamId, n.Team.SpaceUnderSalaryCap);

            await _integrationEventPublishService.AddEventAsync(integrationEvent);
        }
    }
}
