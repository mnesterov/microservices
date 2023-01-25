using MassTransit;
using Teams.Application.IntegrationEvents.Events;

namespace Teams.Infrastructure.IntegrationEvents.Producer
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
