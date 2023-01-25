using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Teams.Application.IntegrationEvents.Events;
using Teams.Infrastructure.IntegrationEvents.EventLog.Models;

namespace Teams.Infrastructure.IntegrationEvents.EventLog
{
    public class IntegrationEventLogService : IIntegrationEventLogService
    {
        private readonly ILogger<IntegrationEventLogService> _logger;
        private readonly AppDbContext _dbContext;
        private readonly DbSet<IntegrationEventLog> _eventLogs;

        public IntegrationEventLogService(
            ILogger<IntegrationEventLogService> logger,
            AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _eventLogs = _dbContext.Set<IntegrationEventLog>();
        }

        public Task AddEventAsync(IntegrationEvent @event)
        {
            var transaction = _dbContext.CurrentTransaction;

            _logger.LogInformation($"--> Add an integration event {@event.GetType()} with id {@event.EventId} and transaction id {transaction.TransactionId}");

            var eventLog = new IntegrationEventLog(@event, transaction.TransactionId);
            _eventLogs.Add(eventLog);
            return _dbContext.SaveChangesAsync();
        }

        public async Task<bool> EventExistAsync(Guid eventId)
        {
            var eventLog = await _eventLogs.FindAsync(eventId);
            return eventLog != null;
        }

        public async Task<ICollection<IntegrationEventLog>> GetReadyToPublishEventLogs(Guid transactionId)
        {
            var eventLogs = await _eventLogs.Where(e => e.TransactionId == transactionId
                && e.Status == IntegrationEventLogStatus.NotPublished).ToListAsync();

            return eventLogs;
        }

        public Task MarkEventAsFailedAsync(Guid eventId)
        {
            return UpdateEventStatus(eventId, IntegrationEventLogStatus.Failed);
        }

        public Task MarkEventAsInProgressAsync(Guid eventId)
        {
            return UpdateEventStatus(eventId, IntegrationEventLogStatus.InProgress);
        }

        public Task MarkEventAsPublishedAsync(Guid eventId)
        {
            return UpdateEventStatus(eventId, IntegrationEventLogStatus.Published);
        }

        #region Private Methods

        private Task UpdateEventStatus(Guid eventId, IntegrationEventLogStatus status)
        {
            _logger.LogInformation($"--> Integration Event {eventId} status updating to {status}");

            var eventLog = _eventLogs.Find(eventId);

            eventLog.Status = status;

            return _dbContext.SaveChangesAsync();
        }

        #endregion
    }
}
