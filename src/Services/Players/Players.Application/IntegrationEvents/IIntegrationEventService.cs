using Players.Application.IntegrationEvents.Events;

namespace Players.Application.IntegrationEvents
{
    public interface IIntegrationEventService
    {
        Task AddEventAsync(IntegrationEvent @event);
        Task PublishEventsAsync(Guid transactionId);
    }
}
