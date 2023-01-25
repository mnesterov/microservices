using System.Text.Json.Serialization;

namespace Players.Application.IntegrationEvents.Events
{
    public abstract class IntegrationEvent : IIntegrationEvent
    {
        public IntegrationEvent()
        {
            EventId = Guid.NewGuid();
            Created = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
        }

        [JsonConstructor]
        public IntegrationEvent(Guid eventId, DateTime created)
        {
            EventId = eventId;
            Created = created;
        }

        public Guid EventId { get; private set; }
        public DateTime Created { get; private set; }
    }
}
