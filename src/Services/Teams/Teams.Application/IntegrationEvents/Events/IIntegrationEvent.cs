namespace Teams.Application.IntegrationEvents.Events
{
    public interface IIntegrationEvent
    {
        Guid EventId { get; }
        DateTime Created { get; }
    }
}
