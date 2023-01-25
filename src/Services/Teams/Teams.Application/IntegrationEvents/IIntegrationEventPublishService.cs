using Teams.Application.IntegrationEvents.Events;

namespace Teams.Application.IntegrationEvents
{
    public interface IIntegrationEventPublishService
    {
        Task AddEventAsync(IntegrationEvent @event);
        Task PublishEventsAsync(Guid transactionId);
    }
}
