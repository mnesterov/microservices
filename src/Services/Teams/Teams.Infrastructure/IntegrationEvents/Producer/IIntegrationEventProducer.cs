using Teams.Application.IntegrationEvents.Events;

namespace Teams.Infrastructure.IntegrationEvents.Producer
{
    public interface IIntegrationEventProducer
    {
        Task Produce<T>(T @event) where T : IntegrationEvent;
    }
}
