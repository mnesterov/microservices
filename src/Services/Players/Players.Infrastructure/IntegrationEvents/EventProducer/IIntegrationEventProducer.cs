using Players.Application.IntegrationEvents.Events;

namespace Players.Infrastructure.IntegrationEvents.EventProducer
{
    public interface IIntegrationEventProducer
    {
        Task Produce<T>(T @event) where T : IntegrationEvent;
    }
}
