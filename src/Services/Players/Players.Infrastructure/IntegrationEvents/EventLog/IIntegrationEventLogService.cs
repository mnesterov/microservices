using Players.Application.IntegrationEvents.Events;

namespace Players.Infrastructure.IntegrationEvents.EventLog
{
    public interface IIntegrationEventLogService
    {
        Task AddEventAsync(IntegrationEvent @event);
        Task MarkEventAsPublishedAsync(Guid eventId);
        Task MarkEventAsInProgressAsync(Guid eventId);
        Task MarkEventAsFailedAsync(Guid eventId);
        Task<ICollection<IntegrationEventLog>> GetReadyToPublishEventLogs(Guid transactionId);
    }
}
