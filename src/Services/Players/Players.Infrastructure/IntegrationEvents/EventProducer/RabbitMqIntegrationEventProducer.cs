using MassTransit;
using Players.Application.IntegrationEvents.Events;

namespace Players.Infrastructure.IntegrationEvents.EventProducer
{
    public class RabbitMqIntegrationEventProducer : IIntegrationEventProducer
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public RabbitMqIntegrationEventProducer(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Produce<T>(T @event) where T : IntegrationEvent
        {
            var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));

            await _publishEndpoint.Publish(@event, cancellationTokenSource.Token);
        }
    }
}
