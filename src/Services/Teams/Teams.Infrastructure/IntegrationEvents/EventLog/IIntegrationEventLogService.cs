using Teams.Application.IntegrationEvents.Events;
using Teams.Infrastructure.IntegrationEvents.EventLog.Models;

namespace Teams.Infrastructure.IntegrationEvents.EventLog
{
    public interface IIntegrationEventLogService
    {
        Task AddEventAsync(IntegrationEvent @event);
        Task<bool> EventExistAsync(Guid eventId);
        Task MarkEventAsPublishedAsync(Guid eventId);
        Task MarkEventAsInProgressAsync(Guid eventId);
        Task MarkEventAsFailedAsync(Guid eventId);
        Task<ICollection<IntegrationEventLog>> GetReadyToPublishEventLogs(Guid transactionId);
    }
}
