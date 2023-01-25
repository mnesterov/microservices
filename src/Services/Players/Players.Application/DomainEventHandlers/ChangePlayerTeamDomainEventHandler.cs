using Microsoft.Extensions.Logging;
using Players.Application.IntegrationEvents;
using Players.Application.IntegrationEvents.Events;
using Players.Domain.Events;

namespace Players.Application.DomainEventHandlers
{
    public class ChangePlayerTeamDomainEventHandler : IDomainEventHandler<ChangePlayerTeamDomainEvent>
    {
        private readonly IIntegrationEventService _integrationEventService;
        private readonly ILogger<ChangePlayerTeamDomainEventHandler> _logger;

        public ChangePlayerTeamDomainEventHandler(
            IIntegrationEventService integrationEventService,
            ILogger<ChangePlayerTeamDomainEventHandler> logger)
        {
            _integrationEventService = integrationEventService;
            _logger = logger;
        }

        public async Task Handle(ChangePlayerTeamDomainEvent n, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"--> Player {n.Player.Id} {n.Player.FullName} has been assigned to team {n.NewTeamId} from team {n.OldTeamId}");

            var integrationEvent = new TradePlayerIntegrationEvent(n.Player.Id, n.NewTeamId, n.OldTeamId, n.Player.SalaryInfo.ContractAnnualSalary);

            await _integrationEventService.AddEventAsync(integrationEvent);
        }
    }
}
