using Microsoft.Extensions.Logging;
using Teams.Application.IntegrationEvents;
using Teams.Application.IntegrationEvents.Events;
using Teams.Infrastructure.IntegrationEvents.EventLog;
using Teams.Infrastructure.IntegrationEvents.Producer;

namespace Teams.Infrastructure.IntegrationEvents
{
    public class IntegrationEventPublishService : IIntegrationEventPublishService
    {
        private readonly IIntegrationEventLogService _integrationEventLogService;
        private readonly IIntegrationEventProducer _producer;
        private readonly ILogger<IntegrationEventPublishService> _logger;

        public IntegrationEventPublishService(
            IIntegrationEventLogService integrationEventLogService,
            IIntegrationEventProducer producer,
            ILogger<IntegrationEventPublishService> logger)
        {
            _integrationEventLogService = integrationEventLogService;
            _logger = logger;
            _producer = producer;
        }

        public Task AddEventAsync(IntegrationEvent @event)
        {
            return _integrationEventLogService.AddEventAsync(@event);
        }

        public async Task PublishEventsAsync(Guid transactionId)
        {
            var pendingEventLogs = await _integrationEventLogService.GetReadyToPublishEventLogs(transactionId);

            foreach (var eventLog in pendingEventLogs)
            {
                try
                {
                    _logger.LogInformation($"--> Publishing {eventLog.EventType} with Id {eventLog.EventId}");

                    await _integrationEventLogService.MarkEventAsInProgressAsync(eventLog.EventId);
                    await _producer.Produce(eventLog.Event);
                    await _integrationEventLogService.MarkEventAsPublishedAsync(eventLog.EventId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"ERROR on Publishing for {eventLog.EventType} with Id {eventLog.EventId}");
                    await _integrationEventLogService.MarkEventAsFailedAsync(eventLog.EventId);
                }
            }
        }
    }
}
