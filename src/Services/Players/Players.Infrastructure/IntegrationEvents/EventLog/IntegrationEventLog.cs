using Players.Application.IntegrationEvents.Events;
using System.Text.Json;

namespace Players.Infrastructure.IntegrationEvents.EventLog
{
    public class IntegrationEventLog
    {
        private IntegrationEvent _event;

        protected IntegrationEventLog() { }

        public IntegrationEventLog(IntegrationEvent @event, Guid transactionId)
        {
            _event = @event;
            EventId = @event.EventId;
            TransactionId = transactionId;
            EventType = @event.GetType().FullName;
            Created = @event.Created;
            Content = JsonSerializer.Serialize(@event, @event.GetType(), new JsonSerializerOptions
            {
                WriteIndented = true
            });

            Status = IntegrationEventLogStatus.NotPublished;
            SendAttempts = 0;
        }

        public Guid EventId { get; private set; }
        public Guid TransactionId { get; private set; }
        public string EventType { get; private set; }
        public DateTime Created { get; private set; }
        public string Content { get; private set; }

        public IntegrationEventLogStatus Status { get; set; }
        public int SendAttempts { get; set; }

        public IntegrationEvent Event
        {
            get
            {
                return _event ??= (IntegrationEvent)JsonSerializer
                    .Deserialize(Content, Type.GetType(EventType), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
        }
    }

    public enum IntegrationEventLogStatus
    {
        NotPublished,
        InProgress,
        Failed,
        Published
    }
}
